using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class X509Data
    {
        [XmlElement("X509IssuerSerial")]
        public List<X509IssuerSerialElement> X509IssuerSerials { get; set; }

        [XmlElement("X509Certificate")]
        public List<X509CertificateElement> X509Certificates { get; set; }
    }
}