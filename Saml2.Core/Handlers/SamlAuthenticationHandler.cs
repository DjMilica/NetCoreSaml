using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Saml2.Core.Handlers
{
    public class SamlAuthenticationHandler : AuthenticationHandler<SamlAuthenticationOptions>
    {
        public SamlAuthenticationHandler(
            IOptionsMonitor<SamlAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            properties = properties ?? new AuthenticationProperties();


            // should create auth request here
        }


    }
}
