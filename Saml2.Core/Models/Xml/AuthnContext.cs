using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class AuthnContext
    {
        [XmlElement(ElementName = SamlElementSelector.AuthnContextClassRef, Namespace = NamespaceConstant.Saml)]
        public AuthnContextClassRef AuthnContextClassRef { get; set; }

        [XmlElement(ElementName = SamlElementSelector.AuthnContextDecl, Namespace = NamespaceConstant.Saml)]
        public AuthnContextDecl AuthnContextDecl { get; set; }

        [XmlElement(ElementName = SamlElementSelector.AuthnContextDeclRef, Namespace = NamespaceConstant.Saml)]
        public AuthnContextDeclRef AuthnContextDeclRef { get; set; }

        [XmlElement(ElementName = SamlElementSelector.AuthenticatingAuthority, Namespace = NamespaceConstant.Saml)]
        public List<AuthenticatingAuthority> AuthenticatingAuthorities { get; set; }
    }
}