using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Constants
{
    public class NameIdFormat
    {
        public const string EMAIL = "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";
        public const string X509_SUBJECT_NAME = "urn:oasis:names:tc:SAML:1.1:nameid-format:X509SubjectName";
        public const string WINDOWS = "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";
        public const string KERBEROS = "urn:oasis:names:tc:SAML:2.0:nameid-format:kerberos";
        public const string ENTITY = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
        public const string PERSISTENT = "urn:oasis:names:tc:SAML:2.0:nameid-format:persistent";
        public const string TRANSIENT = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";
        public const string UNSPECIFIED = "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified";
        public const string UNSUPPORTED = "unsupported";
    }
}
