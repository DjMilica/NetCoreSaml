using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class Base64BinaryElement
    {
        [XmlText(DataType = "base64Binary")]
        public byte[] Value { get; set; }
    }
}