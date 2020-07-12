using Saml2.Core.Constants;
using System;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseRootElement
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdUpperCase)]
        public string Id { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.Version)]
        public string Version { get; set; }

        [XmlAttribute(DataType = "dateTime", AttributeName = SamlAttributeSelector.IssueInstant)]
        public DateTime IssueInstant { get; set; }

        [XmlElement(ElementName = SamlElementSelector.Issuer, Namespace = NamespaceConstant.Saml)]
        public Issuer Issuer { get; set; }

        [XmlElement(ElementName = SamlElementSelector.Signature, Namespace = NamespaceConstant.Dsig)]
        public Signature Signature { get; set; }
    }
}
