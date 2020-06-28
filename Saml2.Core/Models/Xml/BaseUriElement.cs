using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseUriElement
    {
        [XmlText(DataType = "anyURI")]
        public string Value { get; set; }
    }
}