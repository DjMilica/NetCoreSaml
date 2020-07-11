using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SubjectConfirmation
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = "Method")]
        public string Method { get; set; }

        [XmlElement(ElementName = "NameID", Namespace = NamespaceConstant.Saml)]
        public NameId NameId { get; set; }

        [XmlElement(ElementName = "EncryptedID", Namespace = NamespaceConstant.Saml)]
        public EncryptedId EncryptedId { get; set; }

        [XmlElement(ElementName = "SubjectConfirmationData", Namespace = NamespaceConstant.Saml)]
        public SubjectConfirmationData SubjectConfirmationData { get; set; }
    }
}