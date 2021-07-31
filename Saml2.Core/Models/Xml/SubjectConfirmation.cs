using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SubjectConfirmation
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Method)]
        public string Method { get; set; }

        [XmlElement(ElementName = SamlElementSelector.NameId, Namespace = NamespaceConstant.Saml)]
        public NameId NameId { get; set; }

        [XmlElement(ElementName = SamlElementSelector.EncryptedId, Namespace = NamespaceConstant.Saml)]
        public EncryptedID EncryptedId { get; set; }

        [XmlElement(ElementName = SamlElementSelector.SubjectConfirmationData, Namespace = NamespaceConstant.Saml)]
        public SubjectConfirmationData SubjectConfirmationData { get; set; }
    }
}