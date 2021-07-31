using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Subject
    {
        [XmlElement(ElementName = SamlElementSelector.NameId, Namespace = NamespaceConstant.Saml)]
        public NameId NameId { get; set; }

        [XmlElement(ElementName = SamlElementSelector.EncryptedId, Namespace = NamespaceConstant.Saml)]
        public EncryptedID EncryptedId { get; set; }

        [XmlElement(ElementName = SamlElementSelector.SubjectConfirmation, Namespace = NamespaceConstant.Saml)]
        public List<SubjectConfirmation> SubjectConfirmations { get; set; }
    }
}