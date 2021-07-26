using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Helpers;

namespace Saml2.Core.Providers
{
    public interface ISpConfigurationProvider
    {
        string GetEntityId();
        string GetAuthenticationResponseLocation();
        string GetLogoutLocation();
        bool GetWantAssertionsSigned();
        bool GetAuthenticationRequestSigned();
        SignatureAlgorithm GetAuthenticationRequestSigningAlgorithm();
        string GetPrivateKey();
        string GetPublicKey();
        bool GetValidateTimeAttributes();
        int GetMillisecondsSkew();

    }

    public class SpConfigurationProvider: ISpConfigurationProvider
    {
        private readonly ServiceProviderConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SpConfigurationProvider(
            IOptions<SamlConfiguration> options,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.configuration = options.Value.ServiceProviderConfiguration;
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpContext Context => this.httpContextAccessor.HttpContext;

        private HttpRequest Request => Context.Request;

        public string GetEntityId()
        {
            this.Validate();

            return this.configuration.EntityId;
        }

        public string GetAuthenticationResponseLocation()
        {
            this.Validate();

            string authnResponseEndpoint = this.configuration.AuthnResponseEndpoint;

            if (!authnResponseEndpoint.IsNotNullOrWhitspace())
            {
                throw new SamlInternalException("Authentication response endpoint should be defined.");
            }

            string transformedAuthnResponseEndpoint = authnResponseEndpoint.Trim();

            if (!transformedAuthnResponseEndpoint.StartsWith("/"))
            {
                transformedAuthnResponseEndpoint = $"/{transformedAuthnResponseEndpoint}";
            }

            return $"{this.Request.Scheme}://{this.Request.Host}{transformedAuthnResponseEndpoint}";
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

        public SignatureAlgorithm GetAuthenticationRequestSigningAlgorithm()
        {
            this.Validate();

            string name =  this.configuration.AuthnRequestSigningAlgorithm;

            if (name == SignatureAlgorithmConstants.RsaSha256.Name)
            {
                return SignatureAlgorithmConstants.RsaSha256;
            } 
            else if (name == SignatureAlgorithmConstants.RsaSha1.Name)
            {
                return SignatureAlgorithmConstants.RsaSha1;
            } 
            else
            {
                throw new SamlInternalException($"Authn request signature algorithm could be {SignatureAlgorithmConstants.RsaSha256.Name} or {SignatureAlgorithmConstants.RsaSha1.Name}, but it is defined as {name}");
            }
        }

        public string GetPrivateKey()
        {
            this.Validate();

            string privateKeyFilePath = this.configuration.PrivateKeyFilePath;

            if (!privateKeyFilePath.IsNotNullOrWhitspace())
            {
                throw new SamlInternalException("Service provider configuration private key file path is not defined!");
            }

            return FileHelper.Read(privateKeyFilePath);
        }

        public string GetPublicKey()
        {
            this.Validate();

            string publicKeyFilePath = this.configuration.PublicKeyFilePath;

            if (!publicKeyFilePath.IsNotNullOrWhitspace())
            {
                throw new SamlInternalException("Service provider configuration public key file path is not defined!");
            }

            return FileHelper.Read(publicKeyFilePath);
        }

        public bool GetValidateTimeAttributes()
        {
            this.Validate();

            return this.configuration.ValidateTimeAttributes ?? true;
        }

        public int GetMillisecondsSkew()
        {
            this.Validate();

            return this.configuration.MillisecondsSkew ?? 600;
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
