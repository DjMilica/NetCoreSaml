namespace Saml2.Core.Configuration
{
    public class ServiceProviderConfiguration
    {
        public string EntityId { get; set; }
        public bool WantAssertionsSigned { get; set; }
        public bool AuthnRequestsSigned { get; set; }
        public string AuthnResponseEndpoint { get; set; }
        public string LogoutEndpoint { get; set; }
        public string AuthnRequestSigningAlgorithm { get; set; }
        public string PrivateKeyFilePath { get; set; }
        public string PublicKeyFilePath { get; set;  }
        public bool? ValidateTimeAttributes { get; set; }
        public int? MillisecondsSkew { get; set; }
    }
}
