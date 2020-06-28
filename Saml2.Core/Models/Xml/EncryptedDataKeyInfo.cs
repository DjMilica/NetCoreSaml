using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptedDataKeyInfo: KeyInfo
    {
        [XmlElement("EncryptedKey")]
        public List<EncryptedKey> EncryptedKeys { get; set; }
    }
}