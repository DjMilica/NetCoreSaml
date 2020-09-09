using Saml2.Core.Configuration;
using Saml2.Core.Errors;
using Saml2.Core.Providers;

namespace Saml2.Core.Factories
{
    public interface IIdpConfigurationProviderFactory
    {
        IIdpConfigurationProvider Create(SamlAuthenticationOptions options);
        IIdpConfigurationProvider Get();
    }

    public class IdpConfigurationProviderFactory : IIdpConfigurationProviderFactory
    {
        private IIdpConfigurationProvider provider;

        public IIdpConfigurationProvider Create(SamlAuthenticationOptions options)
        {
            this.provider = new IdpConfigurationProvider(options);

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
