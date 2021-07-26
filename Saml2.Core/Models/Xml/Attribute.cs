using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Attribute
    {
        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.Name)]
        public string Name { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.NameFormat)]
        public string NameFormat { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.FriendlyName)]
        public string FriendlyName { get; set; }

        [XmlElement(ElementName = SamlElementSelector.AttributeValue, Namespace = NamespaceConstant.Saml, IsNullable = true)]
        public BaseAttributeValue[] AttributeValues { get; set; }
    }
}