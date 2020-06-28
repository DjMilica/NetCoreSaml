using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptedKey : BaseEncryptedType
    {
        [XmlAttribute(DataType = "string", AttributeName = "Recipient")]
        public string Recipient { get; set; }

        [XmlElement("KeyInfo")]
        public KeyInfo KeyInfo { get; set; }

        [XmlElement("ReferenceList")]
        public ReferenceList ReferenceList { get; set; }

        [XmlElement("CarriedKeyName")]
        public CarriedKeyName CarriedKeyName { get; set; }
    }
}