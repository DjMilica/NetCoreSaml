using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Errors;
using Saml2.Core.Handlers;
using Saml2.Core.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saml2.Core.Factories
{
    public interface IIdpConfigurationProviderFactory
    {
        IIdpConfigurationProvider Create(SamlAuthenticationOptions options);
        IIdpConfigurationProvider Get();

        Task<IIdpConfigurationProvider> CreateWithIdpEntityId(string idpEntityId);
    }

    public class IdpConfigurationProviderFactory : IIdpConfigurationProviderFactory
    {
        private IIdpConfigurationProvider provider;
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;
        private readonly IOptionsMonitor<SamlAuthenticationOptions> optionsMonitor;

        public IdpConfigurationProviderFactory(
            IAuthenticationSchemeProvider authenticationSchemeProvider,
            IOptionsMonitor<SamlAuthenticationOptions> optionsMonitor
        )
        {
            this.authenticationSchemeProvider = authenticationSchemeProvider;
            this.optionsMonitor = optionsMonitor;
        }

        public IIdpConfigurationProvider Create(SamlAuthenticationOptions options)
        {
            this.provider = new IdpConfigurationProvider(options);

            return this.provider;
        }

        public async Task<IIdpConfigurationProvider> CreateWithIdpEntityId(string idpEntityId)
        {
            List<AuthenticationScheme> schemes = (await this.authenticationSchemeProvider.GetAllSchemesAsync()).ToList();

            List<AuthenticationScheme> samlAuthenticationSchemes = schemes.FindAll(x => x.HandlerType.Name == nameof(SamlAuthenticationHandler));

            this.provider = null;

            foreach (AuthenticationScheme authenticationScheme in samlAuthenticationSchemes)
            {
                SamlAuthenticationOptions samlAuthenticationOptions = this.optionsMonitor.Get(authenticationScheme.Name);

                if (samlAuthenticationOptions?.IdentityProviderConfiguration?.EntityId == idpEntityId)
                {
                    this.provider = new IdpConfigurationProvider(samlAuthenticationOptions);
                }
            }

            SamlValidationGuard.NotNull(this.provider, $"Identity provider with sent id ${idpEntityId} is not configured in this application");

            return this.provider;
        }

        public IIdpConfigurationProvider Get()
        {
            if (this.provider == null)
            {
                throw new SamlInternalException("IDP config provider should be initialized before accessing it.");
            }

            return this.provider;
        }
    }
}
