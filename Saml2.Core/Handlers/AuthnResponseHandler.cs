using Microsoft.AspNetCore.Http;
using Saml2.Core.Constants;
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
using System.Text;
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

        public AuthnResponseHandler(
            IHttpContextAccessor httpContextAccessor,
            ISerializeXmlService serializeXmlService,
            AuthnResponseContext authnResponseContext,
            IAuthnResponseValidatorListProvider authnResponseValidatorListProvider,
            IAuthnRequestStore authnRequestStore,
            IAuthnResponseUserDataResolver authnResponseUserDataResolver
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.serializeXmlService = serializeXmlService;
            this.authnResponseContext = authnResponseContext;
            this.authnResponseValidatorListProvider = authnResponseValidatorListProvider;
            this.authnRequestStore = authnRequestStore;
            this.authnResponseUserDataResolver = authnResponseUserDataResolver;
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

            SamlUserData samlUserData = this.authnResponseUserDataResolver.Resolve();

            if (xmlResponseObject.InResponseTo.IsNotNullOrWhitspace())
            {
                await this.authnRequestStore.Remove(xmlResponseObject.InResponseTo);
            }

            return "returnUrl";
        }
    }
}
