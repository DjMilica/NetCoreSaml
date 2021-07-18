using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Constants;
using Saml2.Core.Factories;
using Saml2.Core.Providers;
using Saml2.Core.Services;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Saml2.Core.Handlers
{
    public class SamlAuthenticationHandler : AuthenticationHandler<SamlAuthenticationOptions>, IAuthenticationRequestHandler
    {
        private readonly IAuthnRequestService authnRequestService;
        private readonly IIdpConfigurationProviderFactory idpConfigurationProviderFactory;
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly IAuthnResponseHandler authnResponseHandler;

        public SamlAuthenticationHandler(
            IOptionsMonitor<SamlAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthnRequestService authnRequestService,
            IIdpConfigurationProviderFactory idpConfigurationProviderFactory,
            ISpConfigurationProvider spConfigurationProvider,
            IAuthnResponseHandler authnResponseHandler
        ) : base(options, logger, encoder, clock)
        {
            this.authnRequestService = authnRequestService;
            this.idpConfigurationProviderFactory = idpConfigurationProviderFactory;
            this.spConfigurationProvider = spConfigurationProvider;
            this.authnResponseHandler = authnResponseHandler;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await Context.AuthenticateAsync();
        }

        // This feature is supported by the Authentication middleware which does not invoke any subsequent IAuthenticationHandler 
        // or middleware configured in the request pipeline if the handler returns true
        public async Task<bool> HandleRequestAsync()
        {

            string receivePath = this.spConfigurationProvider.GetAuthenticationResponseLocation();

            if (!receivePath.EndsWith(Request.Path.Value, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (Request.Method != HttpMethods.Post)
            {
                return false;
            }

            string redirectUrl = await this.authnResponseHandler.Handle();

            Context.Response.Redirect(redirectUrl);

            return true;
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
