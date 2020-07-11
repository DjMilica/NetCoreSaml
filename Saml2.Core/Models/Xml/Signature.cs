using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Signature
    {
        [XmlAttribute(DataType = "ID", AttributeName = "ID")]
        public string Id { get; set; }

        [XmlElement(ElementName = "SignedInfo", Namespace = NamespaceConstant.Dsig)]
        public SignedInfo SignedInfo { get; set; }

        [XmlElement(ElementName = "SignatureValue", Namespace = NamespaceConstant.Dsig)]
        public SignatureValue SignatureValue { get; set; }

        [XmlElement(ElementName = "KeyInfo", Namespace = NamespaceConstant.Dsig)]
        public KeyInfo KeyInfo { get; set; }
    }
}
