using Saml2.Core.Configuration;
using Saml2.Core.Errors;

namespace Saml2.Core.Providers
{
    public interface IServiceProviderConfigurationProvider
    {
        string GetEntityId();
        string GetAuthenticationResponseLocation();
        string GetLogoutLocation();
        bool GetWantAssertionsSigned();
        bool GetAuthenticationRequestSigned();

    }

    public class ServiceProviderConfigurationProvider: IServiceProviderConfigurationProvider
    {
        private readonly ServiceProviderConfiguration configuration;

        public ServiceProviderConfigurationProvider(SamlConfiguration configuration)
        {
            this.configuration = configuration.ServiceProviderConfiguration;
        }

        public string GetEntityId()
        {
            this.Validate();

            return this.configuration.EntityId;
        }

        public string GetAuthenticationResponseLocation()
        {
            this.Validate();

            return this.configuration.AuthnResponseEndpoint;
        }

        public string GetLogoutLocation()
        {
            this.Validate();

            return this.configuration.LogoutEndpoint;
        }

        public bool GetWantAssertionsSigned()
        {
            this.Validate();

            return this.configuration.WantAssertionsSigned;
        }

        public bool GetAuthenticationRequestSigned()
        {
            this.Validate();

            return this.configuration.AuthnRequestsSigned;
        }

        private void Validate()
        {
            if (this.configuration == null)
            {
                throw new SamlInternalException("Service provider configuration is not defined in appsettings.json");
            }
        }
    }
}
