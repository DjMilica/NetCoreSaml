using System;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class AuthnStatement
    {
        [XmlAttribute(DataType = "dateTime", AttributeName = "AuthnInstant")]
        public DateTime AuthnInstant { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "SessionIndex")]
        public string SessionIndex { get; set; }

        [XmlAttribute(DataType = "dateTime", AttributeName = "SessionNotOnOrAfter")]
        public DateTime  SessionNotOnOrAfter{ get; set; }

        [XmlElement("SubjectLocality")]
        public SubjectLocality SubjectLocality { get; set; }

        [XmlElement("AuthnContext")]
        public AuthnContext AuthnContext { get; set; }
    }
}