namespace Saml2.Core
{
    public class SamlElementSelector
    {
        public const string AuthnResponse = "Response";
        public const string Assertion = "Assertion";
        public const string Attribute = "Attribute";
        public const string AttributeStatement = "AttributeStatement";
        public const string Audience = "Audience";
        public const string AudienceRestriction = "AudienceRestriction";
        public const string AttributeValue = "AttributeValue";
        public const string AuthenticatingAuthority = "AuthenticatingAuthority";
        public const string AuthnContext = "AuthnContext";
        public const string AuthnContextClassRef = "AuthnContextClassRef";
        public const string AuthnContextDeclRef = "AuthnContextDeclRef";
        public const string AuthnContextDecl = "AuthnContextDecl";
        public const string AuthnStatement = "AuthnStatement";
        public const string CanonicalizationMethod = "CanonicalizationMethod";
        public const string CarriedKeyName = "CarriedKeyName";
        public const string CipherData = "CipherData";
        public const string CipherReference = "CipherReference";
        public const string CipherValue = "CipherValue";
        public const string Conditions = "Conditions";
        public const string DataReference = "DataReference";
        public const string DigestMethod = "DigestMethod";
        public const string DigestValue = "DigestValue";
        public const string EncryptedAssertion = "EncryptedAssertion";
        public const string EncryptedAttribute = "EncryptedAttribute";
        public const string EncryptedData = "EncryptedData";
        public const string EncryptedId = "EncryptedID";
        public const string EncryptedKey = "EncryptedKey";
        public const string EncryptionMethod = "EncryptionMethod";
        public const string EncryptionProperties = "EncryptionProperties";
        public const string EncryptionProperty = "EncryptionProperty";
        public const string Issuer = "Issuer";
        public const string KeyInfo = "KeyInfo";
        public const string KeyName = "KeyName";
        public const string KeyReference = "KeyReference";
        public const string KeySize = "KeySize";
        public const string NameId = "NameID";
        public const string OaepParams = "OAEPparams";
        public const string OneTimeUse = "OneTimeUse";
        public const string ProxyRestriction = "ProxyRestriction";
        public const string Reference = "Reference";
        public const string ReferenceList = "ReferenceList";
        public const string Signature = "Signature";
        public const string SignatureMethod = "SignatureMethod";
        public const string SignatureValue = "SignatureValue";
        public const string SignedInfo = "SignedInfo";
        public const string Status = "Status";
        public const string StatusCode = "StatusCode";
        public const string StatusDetail = "StatusDetail";
        public const string StatusMessage = "StatusMessage";
        public const string Subject = "Subject";
        public const string SubjectConfirmation = "SubjectConfirmation";
        public const string SubjectConfirmationData = "SubjectConfirmationData";
        public const string SubjectLocality = "SubjectLocality";
        public const string Transform = "Transform";
        public const string Transforms = "Transforms";
        public const string X509Certificate = "X509Certificate";
        public const string X509Data = "X509Data";
        public const string X509IssuerName = "X509IssuerName";
        public const string X509IssuerSerial = "X509IssuerSerial";
        public const string X509SerialNumber = "X509SerialNumber";

        public const string AuthnRequest = "AuthnRequest";
        public const string NameIdPolicy = "NameIDPolicy";
    }

    public class SamlAttributeSelector
    {
        public const string IdUpperCase = "ID";
        public const string IssueInstant = "IssueInstant";
        public const string InResponseTo = "InResponseTo";
        public const string Destination = "Destination";
        public const string Version = "Version";
        public const string Name = "Name";
        public const string NameFormat = "NameFormat";
        public const string FriendlyName = "FriendlyName";
        public const string AuthnInstant = "AuthnInstant";
        public const string SessionIndex = "SessionIndex";
        public const string SessionNotOnOrAfter = "SessionNotOnOrAfter";
        public const string IdLowerCase = "Id";
        public const string Type = "Type";
        public const string MimeType = "MimeType";
        public const string Encoding = "Encoding";
        public const string NameQualifier = "NameQualifier";
        public const string SpNameQualifier = "SPNameQualifier";
        public const string Format = "Format";
        public const string SpProvidedId = "SPProvidedID";
        public const string Uri = "URI";
        public const string Algorithm = "Algorithm";
        public const string NotBefore = "NotBefore";
        public const string NotOnOrAfter = "NotOnOrAfter";
        public const string Recipient = "Recipient";
        public const string Target = "Target";
        public const string Count = "Count";
        public const string Value = "Value";
        public const string Method = "Method";
        public const string Address = "Address";
        public const string DnsName = "DNSName";
        public const string ForceAuthn = "ForceAuthn";
        public const string IsPassive = "IsPassive";
        public const string ProtocolBinding = "ProtocolBinding";
        public const string AssertionConsumerServiceUrl = "AssertionConsumerServiceURL";
        public const string AllowCreate = "AllowCreate";
        public const string XsiNil = "nil";
    }
}
