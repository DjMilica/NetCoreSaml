using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptionProperties
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement("EncryptionProperty")]
        public List<EncryptionProperty> EncryptionPropertyList { get; set; }
    }
}