using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlRoot(ElementName = SamlElementSelector.Assertion, Namespace = NamespaceConstant.Saml)]
    public class Assertion: BaseRootElement
    {
        [XmlElement(ElementName = SamlElementSelector.Subject, Namespace = NamespaceConstant.Saml)]
        public Subject Subject { get; set; }

        [XmlElement(ElementName = SamlElementSelector.Conditions, Namespace = NamespaceConstant.Saml)]
        public Conditions Conditions { get; set; }

        [XmlElement(ElementName = SamlElementSelector.AuthnStatement, Namespace = NamespaceConstant.Saml)]
        public List<AuthnStatement> AuthnStatements { get; set; }

        [XmlElement(ElementName = SamlElementSelector.AttributeStatement, Namespace = NamespaceConstant.Saml)]
        public List<AttributeStatement> AttributeStatements { get; set; }
    }
}
