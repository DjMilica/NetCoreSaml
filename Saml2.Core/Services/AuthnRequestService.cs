
using Microsoft.Extensions.Logging;
using Saml2.Core.Constants;
using Saml2.Core.Enums;
using Saml2.Core.Factories;
using Saml2.Core.Models;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System.Xml;

namespace Saml2.Core.Services
{
    public interface IAuthnRequestService
    {
        string CreateRedirectUrl();
        string CreatePostData();
    }

    public class AuthnRequestService: IAuthnRequestService
    {
        private readonly ILogger logger;
        private readonly IAuthnRequestFactory authnRequestFactory;
        private readonly IAuthnRequestXmlProvider authnRequestXmlProvider;
        private readonly ISamlRedirectDataFactory samlRedirectDataFactory;
        private readonly ISpConfigurationProvider spConfigurationProvider;

        public AuthnRequestService(
            ILogger<AuthnRequestService> logger,
            IAuthnRequestFactory authnRequestFactory,
            IAuthnRequestXmlProvider authnRequestXmlProvider,
            ISamlRedirectDataFactory samlRedirectDataFactory,
            ISpConfigurationProvider spConfigurationProvider
        )
        {
            this.logger = logger;
            this.authnRequestFactory = authnRequestFactory;
            this.authnRequestXmlProvider = authnRequestXmlProvider;
            this.samlRedirectDataFactory = samlRedirectDataFactory;
            this.spConfigurationProvider = spConfigurationProvider;
        }

        public string CreateRedirectUrl()
        {
            AuthnRequest request = this.authnRequestFactory.Create();

            string requestXml = this.authnRequestXmlProvider.Get(request);

            bool signRequest = this.spConfigurationProvider.GetAuthenticationRequestSigned();

            SignatureAlgorithm signatureAlgorithm = signRequest ? this.spConfigurationProvider.GetAuthenticationRequestSigningAlgorithm() : null;

            SamlRedirectData samlRedirectData = this.samlRedirectDataFactory.Create(request.Destination, requestXml, CorrespondenceType.Request, signRequest, null, signatureAlgorithm);

            return samlRedirectData.ToRedirectUrl();
        }

        public string CreatePostData()
        {
            throw new System.NotImplementedException();
        }
    }
}
