using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseEncryptedType
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdLowerCase)]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Type)]
        public string Type { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.MimeType)]
        public string MimeType { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Encoding)]
        public string Encoding { get; set; }

        [XmlElement(ElementName = SamlElementSelector.EncryptionMethod, Namespace = NamespaceConstant.Xenc)]
        public EncryptionMethod EncryptionMethod { get; set; }

        [XmlElement(ElementName = SamlElementSelector.CipherData, Namespace = NamespaceConstant.Xenc)]
        public CipherData CipherData { get; set; }

        [XmlElement(ElementName = SamlElementSelector.EncryptionProperties, Namespace = NamespaceConstant.Xenc)]
        public EncryptionProperties EncryptionProperties { get; set; }
    }
}