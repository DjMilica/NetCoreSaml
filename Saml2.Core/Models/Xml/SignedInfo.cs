using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SignedInfo
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement("CanonicalizationMethod")]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }

        [XmlElement("SignatureMethod")]
        public SignatureMethod SignatureMethod { get; set; }

        [XmlElement("Reference")]
        public List<Reference> References { get; set; }
    }
}