using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptedData: BaseEncryptedType
    {
        [XmlElement("KeyInfo")]
        public EncryptedDataKeyInfo KeyInfo { get; set; }
    }
}