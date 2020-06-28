using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SubjectConfirmation
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = "Method")]
        public string Method { get; set; }

        [XmlElement("NameID")]
        public NameId NameId { get; set; }

        [XmlElement("EncryptedID")]
        public EncryptedId EncryptedId { get; set; }

        [XmlElement("SubjectConfirmationData")]
        public SubjectConfirmationData SubjectConfirmationData { get; set; }
    }
}