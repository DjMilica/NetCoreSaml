using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Transform: BaseStringElement
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Algorithm)]
        public string Algorithm { get; set; }
    }
}