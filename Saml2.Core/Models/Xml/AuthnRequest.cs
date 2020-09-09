using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlRoot(ElementName = SamlElementSelector.AuthnRequest, Namespace = NamespaceConstant.Samlp)]
    public class AuthnRequest: BaseCorrespondance
    {
        [XmlAttribute(DataType = "boolean", AttributeName = SamlAttributeSelector.ForceAuthn)]
        public bool ForceAuthn { get; set; }

        [XmlAttribute(DataType = "boolean", AttributeName = SamlAttributeSelector.IsPassive)]
        public bool IsPassive { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.ProtocolBinding)]
        public string ProtocolBinding { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.AssertionConsumerServiceUrl)]
        public string AssertionConsumerServiceUrl { get; set; }

        [XmlElement(ElementName = SamlElementSelector.Subject, Namespace = NamespaceConstant.Saml)]
        public Subject Subject { get; set; }

        [XmlElement(ElementName = SamlElementSelector.NameIdPolicy, Namespace = NamespaceConstant.Samlp)]
        public NameIdPolicy NameIdPolicy { get; set; }
    }
}
