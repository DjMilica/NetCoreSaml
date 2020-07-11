using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class X509IssuerSerialElement
    {
        [XmlElement(ElementName = "X509IssuerName", Namespace = NamespaceConstant.Dsig)]
        public X509IssuerNameElement X509IssuerName{ get; set; }

        [XmlElement(ElementName = "X509SerialNumber", Namespace = NamespaceConstant.Dsig)]
        public X509SerialNumberElement X509SerialNumber{ get; set; }
    }
}