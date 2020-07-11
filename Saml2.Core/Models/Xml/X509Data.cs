using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class X509Data
    {
        [XmlElement(ElementName = "X509IssuerSerial", Namespace = NamespaceConstant.Dsig)]
        public List<X509IssuerSerialElement> X509IssuerSerials { get; set; }

        [XmlElement(ElementName = "X509Certificate", Namespace = NamespaceConstant.Dsig)]
        public List<X509CertificateElement> X509Certificates { get; set; }
    }
}