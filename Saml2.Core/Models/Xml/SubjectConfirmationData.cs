using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SubjectConfirmationData: BaseStringElement
    {
        [XmlAttribute(DataType = "dateTime", AttributeName = "NotBefore")]
        public DateTime NotBefore { get; set; }

        [XmlAttribute(DataType = "dateTime", AttributeName = "NotOnOrAfter")]
        public DateTime NotOnOrAfter { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "Recipient")]
        public string Recipient { get; set; }

        [XmlAttribute(DataType = "NCName", AttributeName = "InResponseTo")]
        public string InResponseTo { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "Address")]
        public string Address { get; set; }

        [XmlAnyAttribute]
        public List<string> OtherAttributes { get; set; }
    }
}