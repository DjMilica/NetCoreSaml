using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Assertion: BaseRootElement
    {
        [XmlElement(ElementName = "Subject", Namespace = NamespaceConstant.Saml)]
        public Subject Subject { get; set; }

        [XmlElement(ElementName = "Conditions", Namespace = NamespaceConstant.Saml)]
        public Conditions Condition { get; set; }

        [XmlElement(ElementName = "AuthnStatement", Namespace = NamespaceConstant.Saml)]
        public List<AuthnStatement> AuthnStatements { get; set; }

        [XmlElement(ElementName = "AttributeStatement", Namespace = NamespaceConstant.Saml)]
        public List<AttributeStatement> AttributeStatements { get; set; }
    }
}
