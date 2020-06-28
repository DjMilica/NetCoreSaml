using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseEncryptedElement
    {
        [XmlElement("EncryptedData")]
        public EncryptedData EncryptedData { get; set; }

        [XmlElement("EncryptedKey")]
        public List<EncryptedKey> EncryptedKey { get; set; }
    }
}
