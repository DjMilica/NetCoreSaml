using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Signature
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdUpperCase)]
        public string Id { get; set; }

        [XmlElement(ElementName = SamlElementSelector.SignedInfo, Namespace = NamespaceConstant.Dsig)]
        public SignedInfo SignedInfo { get; set; }

        [XmlElement(ElementName = SamlElementSelector.SignatureValue, Namespace = NamespaceConstant.Dsig)]
        public SignatureValue SignatureValue { get; set; }

        [XmlElement(ElementName = SamlElementSelector.KeyInfo, Namespace = NamespaceConstant.Dsig)]
        public KeyInfo KeyInfo { get; set; }
    }
}
