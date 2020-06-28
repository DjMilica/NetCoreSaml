using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Subject
    {
        [XmlElement("NameID")]
        public NameId NameId { get; set; }

        [XmlElement("EncryptedID")]
        public EncryptedId EncryptedId { get; set; }

        [XmlElement("SubjectConfirmation")]
        public List<SubjectConfirmation> SubjectConfirmations { get; set; }
    }
}