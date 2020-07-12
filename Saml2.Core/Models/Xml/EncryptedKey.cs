using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptedKey : BaseEncryptedType
    {
        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.Recipient)]
        public string Recipient { get; set; }

        [XmlElement(ElementName = SamlElementSelector.KeyInfo, Namespace = NamespaceConstant.Dsig)]
        public KeyInfo KeyInfo { get; set; }

        [XmlElement(ElementName = SamlElementSelector.ReferenceList, Namespace = NamespaceConstant.Xenc)]
        public ReferenceList ReferenceList { get; set; }

        [XmlElement(ElementName = SamlElementSelector.CarriedKeyName, Namespace = NamespaceConstant.Xenc)]
        public CarriedKeyName CarriedKeyName { get; set; }
    }
}