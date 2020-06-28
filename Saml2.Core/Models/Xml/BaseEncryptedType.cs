using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseEncryptedType
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "Type")]
        public string Type { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "MimeType")]
        public string MimeType { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "Encoding")]
        public string Encoding { get; set; }

        [XmlElement("EncryptionMethod")]
        public EncryptionMethod EncryptionMethod { get; set; }

        [XmlElement("CipherData")]
        public CipherData CipherData { get; set; }

        [XmlElement("EncryptionProperties")]
        public EncryptionProperties EncryptionProperties { get; set; }
    }
}