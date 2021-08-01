using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Saml2.Core.Constants;
using Saml2.Core.Encoders;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using Saml2.Core.Resolvers;
using Saml2.Core.Services;
using Saml2.Core.Stores;
using Saml2.Core.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Saml2.Core.Handlers
{
    public interface IAuthnResponseHandler
    {
        Task<string> Handle();
    }

    public class AuthnResponseHandler: IAuthnResponseHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISerializeXmlService serializeXmlService;
        private readonly AuthnResponseContext authnResponseContext;
        private readonly IAuthnResponseValidatorListProvider authnResponseValidatorListProvider;
        private readonly IAuthnRequestStore authnRequestStore;
        private readonly IAuthnResponseUserDataResolver authnResponseUserDataResolver;
        private readonly ISamlEncoder samlEncoder;

        public AuthnResponseHandler(
            IHttpContextAccessor httpContextAccessor,
            ISerializeXmlService serializeXmlService,
            AuthnResponseContext authnResponseContext,
            IAuthnResponseValidatorListProvider authnResponseValidatorListProvider,
            IAuthnRequestStore authnRequestStore,
            IAuthnResponseUserDataResolver authnResponseUserDataResolver,
            ISamlEncoder samlEncoder
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.serializeXmlService = serializeXmlService;
            this.authnResponseContext = authnResponseContext;
            this.authnResponseValidatorListProvider = authnResponseValidatorListProvider;
            this.authnRequestStore = authnRequestStore;
            this.authnResponseUserDataResolver = authnResponseUserDataResolver;
            this.samlEncoder = samlEncoder;
        }

        private HttpContext Context => this.httpContextAccessor.HttpContext;

        private HttpRequest Request => Context.Request;

        public async Task<string> Handle()
        {
            IFormCollection form = Request.Form;

            SamlValidationGuard.NotNull(
                form,
                SamlValidationErrors.AuthnResponseBodyShouldNotBeNull
            );

            string encodedResponse = form[SamlConstant.SamlResponse];
            string encodedRelayState = form[SamlConstant.RelayState];

            SamlValidationGuard.NotNull(
                encodedResponse,
                SamlValidationErrors.AuthnResponseShouldNotBeNull
            );

            byte[] decodedBytes = Convert.FromBase64String(encodedResponse);
            string decodedResponse = Encoding.UTF8.GetString(decodedBytes);
            AuthnResponse xmlResponseObject = this.serializeXmlService.Deserialize<AuthnResponse>(decodedResponse);

            SamlValidationGuard.NotNull(xmlResponseObject, "Received serialized SAML authn xml response should not ne null");

            this.authnResponseContext.StringifiedResponse = decodedResponse;
            this.authnResponseContext.Response = xmlResponseObject;

            IList<ISamlAuthnResponseValidator> validators = this.authnResponseValidatorListProvider.Get();

            foreach(ISamlAuthnResponseValidator validator in validators)
            {
                await validator.Validate(xmlResponseObject);
            }

            string returnUrl = await this.SignIn(encodedRelayState);

            if (xmlResponseObject.InResponseTo.IsNotNullOrWhitspace())
            {
                await this.authnRequestStore.Remove(xmlResponseObject.InResponseTo);
            }

            return returnUrl;
        }

        private async Task<string> SignIn(string encodedRelayState)
        {
            SamlUserData samlUserData = this.authnResponseUserDataResolver.Resolve();
            var identity = new ClaimsIdentity(samlUserData.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            string returnUrl = null;

            if (encodedRelayState.IsNotNullOrWhitspace())
            {
                string relayState = this.samlEncoder.Base64DecodeAndInflate(encodedRelayState);
                RelayStateData relayStateData = JsonSerializer.Deserialize<RelayStateData>(relayState);
                await this.Context.SignInAsync(principal, relayStateData.AuthenticationProperties);
                returnUrl = relayStateData.ReturnUrl;
            }
            else
            {
                await this.Context.SignInAsync(principal);
            }


            return returnUrl ?? this.authnResponseContext.IdpConfigurationProvider.GetAuthnRedirectUrl();
        }
    }
}
