using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class X509IssuerSerialElement
    {
        [XmlElement("X509IssuerName")]
        public X509IssuerNameElement X509IssuerName{ get; set; }

        [XmlElement("X509SerialNumber")]
        public X509SerialNumberElement X509SerialNumber{ get; set; }
    }
}