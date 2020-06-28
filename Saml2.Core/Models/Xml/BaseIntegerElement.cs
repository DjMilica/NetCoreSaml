using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseIntegerElement
    {
        [XmlText(DataType = "integer")]
        public string Value { get; set; }
    }
}