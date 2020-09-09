
using Microsoft.Extensions.Logging;
using Saml2.Core.Factories;
using Saml2.Core.Models.Xml;

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

        public AuthnRequestService(
            ILogger<AuthnRequestService> logger,
            IAuthnRequestFactory authnRequestFactory
        )
        {
            this.logger = logger;
            this.authnRequestFactory = authnRequestFactory;
        }

        public string CreateRedirectUrl()
        {
            AuthnRequest request = this.authnRequestFactory.Create();

            return "someUrl";
        }

        public string CreatePostData()
        {
            throw new System.NotImplementedException();
        }
    }
}
