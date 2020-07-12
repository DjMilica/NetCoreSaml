using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class StatusCode
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Value)]
        public string Value { get; set; }

        [XmlElement(ElementName = SamlElementSelector.StatusCode, Namespace = NamespaceConstant.Samlp)]
        public StatusCode SubCode { get; set; }
    }
}