using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class StatusCode
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = "Value")]
        public string Value { get; set; }

        [XmlElement("StatusCode")]
        public StatusCode SubCode { get; set; }
    }
}