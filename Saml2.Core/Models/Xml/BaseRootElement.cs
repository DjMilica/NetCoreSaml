using System;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseRootElement
    {
        [XmlAttribute(DataType = "ID", AttributeName = "ID")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "Version")]
        public string Version { get; set; }

        [XmlAttribute(DataType = "dateTime", AttributeName = "IssuerInstant")]
        public DateTime IssueInstant { get; set; }

        [XmlElement("Issuer")]
        public Issuer Issuer { get; set; }

        [XmlElement("Signature")]
        public Signature Signature { get; set; }
    }
}
