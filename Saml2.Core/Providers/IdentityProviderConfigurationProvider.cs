using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Errors;

namespace Saml2.Core.Providers
{
    public interface IIdentityProviderConfigurationProvider
    {
        string GetEntityId();
    }

    public class IdentityProviderConfigurationProvider: IIdentityProviderConfigurationProvider
    {
        private readonly IdentityProviderConfiguration configuration;

        public IdentityProviderConfigurationProvider(
            IOptionsMonitor<SamlAuthenticationOptions> options
        )
        {
            this.configuration = options.CurrentValue.IdentityProviderConfiguration;
        }

        public string GetEntityId()
        {
            this.Validate();

            return this.configuration.EntityId;
        }

        private void Validate()
        {
            if (this.configuration == null)
            {
                throw new SamlInternalException("Identity provider configuration is not defined in authentication options");
            }
        }
    }
}
