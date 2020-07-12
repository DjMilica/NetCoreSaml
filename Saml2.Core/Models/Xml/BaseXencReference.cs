using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseXencReference
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Uri)]
        public string Uri { get; set; }

        [XmlAnyElement]
        public object[] OtherElements { get; set; }
    }
}