using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class KeyInfo
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement("KeyName")]
        public KeyName KeyName { get; set; }

        [XmlElement("X509Data")]
        public X509Data X509Data { get; set; }
    }
}