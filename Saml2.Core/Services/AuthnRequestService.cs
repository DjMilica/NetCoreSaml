
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Saml2.Core.Constants;
using Saml2.Core.Enums;
using Saml2.Core.Factories;
using Saml2.Core.Models;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using Saml2.Core.Stores;
using System.Text.Json;
using System.Threading.Tasks;

namespace Saml2.Core.Services
{
    public interface IAuthnRequestService
    {
        Task<string> CreateRedirectUrl(AuthenticationProperties authenticationProperties);
        string CreatePostData();
    }

    public class AuthnRequestService : IAuthnRequestService
    {
        private readonly ILogger logger;
        private readonly IAuthnRequestFactory authnRequestFactory;
        private readonly IAuthnRequestXmlProvider authnRequestXmlProvider;
        private readonly ISamlRedirectDataFactory samlRedirectDataFactory;
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly IAuthnRequestStore authnRequestStore;

        public AuthnRequestService(
            ILogger<AuthnRequestService> logger,
            IAuthnRequestFactory authnRequestFactory,
            IAuthnRequestXmlProvider authnRequestXmlProvider,
            ISamlRedirectDataFactory samlRedirectDataFactory,
            ISpConfigurationProvider spConfigurationProvider,
            IAuthnRequestStore authnRequestStore
        )
        {
            this.logger = logger;
            this.authnRequestFactory = authnRequestFactory;
            this.authnRequestXmlProvider = authnRequestXmlProvider;
            this.samlRedirectDataFactory = samlRedirectDataFactory;
            this.spConfigurationProvider = spConfigurationProvider;
            this.authnRequestStore = authnRequestStore;
        }

        public async Task<string> CreateRedirectUrl(AuthenticationProperties authenticationProperties)
        {
            AuthnRequest request = this.authnRequestFactory.Create();

            string requestXml = this.authnRequestXmlProvider.Get(request);

            bool signRequest = this.spConfigurationProvider.GetAuthenticationRequestSigned();

            SignatureAlgorithm signatureAlgorithm = signRequest ? this.spConfigurationProvider.GetAuthenticationRequestSigningAlgorithm() : null;
            RelayStateData relayStateData = new RelayStateData() { ReturnUrl = authenticationProperties.RedirectUri, AuthenticationProperties = authenticationProperties };

            SamlRedirectData samlRedirectData = this.samlRedirectDataFactory.Create(
                request.Destination,
                requestXml,
                CorrespondenceType.Request,
                signRequest,
                JsonSerializer.Serialize(relayStateData),
                signatureAlgorithm
            );

            await this.authnRequestStore.Insert(request.Id);

            return samlRedirectData.ToRedirectUrl();
        }

        public string CreatePostData()
        {
            throw new System.NotImplementedException();
        }
    }
}
