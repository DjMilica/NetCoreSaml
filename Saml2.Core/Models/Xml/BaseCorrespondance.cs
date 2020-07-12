using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseCorrespondance: BaseRootElement
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Destination)]
        public string Destination { get; set; }
    }
}
