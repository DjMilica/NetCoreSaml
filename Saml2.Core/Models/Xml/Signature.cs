using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Signature
    {
        [XmlAttribute(DataType = "ID", AttributeName = "ID")]
        public string Id { get; set; }

        [XmlElement("SignedInfo")]
        public SignedInfo SignedInfo { get; set; }

        [XmlElement("SignatureValue")]
        public SignatureValue SignatureValue { get; set; }

        [XmlElement("KeyInfo")]
        public KeyInfo KeyInfo { get; set; }
    }
}
