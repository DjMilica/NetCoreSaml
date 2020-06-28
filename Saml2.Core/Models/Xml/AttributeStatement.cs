using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class AttributeStatement
    {
        [XmlElement("Attribute")]
        public List<Attribute> Attributes { get; set; }

        [XmlElement("EncryptedAttribute")]
        public List<EncryptedAttribute> EncryptedAttributes { get; set; }
    }
}