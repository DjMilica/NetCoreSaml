using Saml2.Core.Configuration;
using Saml2.Core.Enums;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Helpers;

namespace Saml2.Core.Providers
{
    public interface IIdpConfigurationProvider
    {
        string GetEntityId();
        BindingType GetAuthnRequestBinding();
        BindingType GetLogoutRequestBinding();
        int GetMillisecondsSkew();
        string GetRedirectBindingAuthnEndpoint();
        string GetPublicKey();
    }

    public class IdpConfigurationProvider: IIdpConfigurationProvider
    {
        private readonly IdentityProviderConfiguration configuration;

        public IdpConfigurationProvider(
            SamlAuthenticationOptions options
        )
        {
            this.configuration = options.IdentityProviderConfiguration;
        }


        public string GetEntityId()
        {
            return this.configuration.EntityId;
        }

        public BindingType GetAuthnRequestBinding()
        {
            return this.configuration.AuthnRequestBinding ?? BindingType.HttpRedirect;
        }

        public BindingType GetLogoutRequestBinding()
        {
            return this.configuration.LogoutRequestBinding ?? BindingType.HttpRedirect;
        }

        public int GetMillisecondsSkew()
        {
            return this.configuration.MillisecondsSkew ?? 600;
        }

        public bool GetUseNameIdAsSpUserId()
        {
            return this.configuration.UseNameIdAsSpUserId ?? true;
        }

        public string GetRedirectBindingAuthnEndpoint()
        {
            string endpoint = this.configuration.HttpRedirectSingleSignOnService;

            if (endpoint == null)
            {
                throw new SamlInternalException("Cannot find IDP authn endpoint for http redirect binding");
            }

            return endpoint;
        }

        public string GetPublicKey()
        {
            string publicKeyFilePath = this.configuration.PublicKeyPath;

            if (!publicKeyFilePath.IsNotNullOrWhitspace())
            {
                throw new SamlInternalException("Idp configuration public key file path is not defined!");
            }

            return FileHelper.Read(publicKeyFilePath);
        }
     }
}
