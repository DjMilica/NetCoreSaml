using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Factories;
using Saml2.Core.Providers;
using Saml2.Core.Services;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Saml2.Core.Handlers
{
    public class SamlAuthenticationHandler : AuthenticationHandler<SamlAuthenticationOptions>
    {
        private readonly IAuthnRequestService authnRequestService;
        private readonly IIdpConfigurationProviderFactory idpConfigurationProviderFactory;

        public SamlAuthenticationHandler(
            IOptionsMonitor<SamlAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthnRequestService authnRequestService,
            IIdpConfigurationProviderFactory idpConfigurationProviderFactory
        ) : base(options, logger, encoder, clock)
        {
            this.authnRequestService = authnRequestService;
            this.idpConfigurationProviderFactory = idpConfigurationProviderFactory;

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            properties ??= new AuthenticationProperties();

            IIdpConfigurationProvider idpConfigProvider = 
                this.idpConfigurationProviderFactory.Create(Options);

            this.Logger.LogInformation($"Started with creating authentication request for {idpConfigProvider.GetEntityId()} idp.");

            // should create auth request here
            if (idpConfigProvider.GetAuthnRequestBinding() == Enums.BindingType.HttpRedirect)
            {
                string redirectUrl = this.authnRequestService.CreateRedirectUrl();

                Context.Response.Redirect(redirectUrl);
            }

            return;
        }
    }
}
