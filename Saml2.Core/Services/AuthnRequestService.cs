
using Microsoft.Extensions.Logging;
using Saml2.Core.Providers;

namespace Saml2.Core.Services
{
    public interface IAuthnRequestService
    {
        string CreateRedirectUrl();
    }

    public class AuthnRequestService: IAuthnRequestService
    {
        private readonly ILogger logger;
        private readonly IIdentityProviderConfigurationProvider identityProviderConfigurationProvider;

        public AuthnRequestService(
            ILogger<AuthnRequestService> logger,
            IIdentityProviderConfigurationProvider identityProviderConfigurationProvider
        )
        {
            this.logger = logger;
            this.identityProviderConfigurationProvider = identityProviderConfigurationProvider;
        }

        public string CreateRedirectUrl()
        {
            string entityId = this.identityProviderConfigurationProvider.GetEntityId();
            return "someUrl";
        }
    }
}
