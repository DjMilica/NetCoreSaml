using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Status
    {
        [XmlElement("StatusCode")]
        public StatusCode StatusCode { get; set; }

        [XmlElement("StatusMessage")]
        public StatusMessage StatusMessage { get; set; }

        [XmlElement("StatusDetail")]
        public StatusDetail StatusDetail { get; set; }
    }
}
