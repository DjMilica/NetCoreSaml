namespace Saml2.Core.Constants
{
    public class SamlConstant
    {
        public const string Version = "2.0";
        public const string BindingTypeHttpRedirect = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect";
        public const string BindingTypeHttpPost = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST";
        public const string SamlRequest = "SAMLRequest";
        public const string SamlResponse = "SAMLResponse";
        public const string RelayState = "RelayState";

        public const string BearerConfirmationMethod = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
        public const string HokConfirmationMethod = "urn:oasis:names:tc:SAML:2.0:cm:holder-of-key";
        public const string SenderVoucesConfirmationMethod = "urn:oasis:names:tc:SAML:2.0:cm:sender-vouches";
    }
}
