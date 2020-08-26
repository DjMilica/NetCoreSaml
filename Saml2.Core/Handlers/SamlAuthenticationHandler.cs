using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Factories;
using Saml2.Core.Services;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Saml2.Core.Handlers
{
    public class SamlAuthenticationHandler : AuthenticationHandler<SamlAuthenticationOptions>
    {
        private readonly IAuthnRequestService authnRequestService;

        public SamlAuthenticationHandler(
            IOptionsMonitor<SamlAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthnRequestService authnRequestService
        ) : base(options, logger, encoder, clock)
        {
            this.authnRequestService = authnRequestService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            properties = properties ?? new AuthenticationProperties();

            // should create auth request here
            string redirectUrl = this.authnRequestService.CreateRedirectUrl();

            Context.Response.Redirect(redirectUrl);

            return;
        }


    }
}
