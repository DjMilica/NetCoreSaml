using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class AuthnContext
    {
        [XmlElement("AuthnContextClassRef")]
        public AuthnContextClassRef AuthnContextClassRef { get; set; }

        [XmlElement("AuthnContextDecl")]
        public AuthnContextDecl AuthnContextDecl { get; set; }

        [XmlElement("AuthnContextDeclRef")]
        public AuthnContextDeclRef AuthnContextDeclRef { get; set; }

        [XmlElement("AuthenticatingAuthority")]
        public List<AuthenticatingAuthority> AuthenticatingAuthorities { get; set; }
    }
}