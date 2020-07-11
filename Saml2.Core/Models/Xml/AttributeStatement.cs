using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class AttributeStatement
    {
        [XmlElement(ElementName = "Attribute", Namespace = NamespaceConstant.Saml)]
        public List<Attribute> Attributes { get; set; }

        [XmlElement(ElementName = "EncryptedAttribute", Namespace = NamespaceConstant.Saml)]
        public List<EncryptedAttribute> EncryptedAttributes { get; set; }
    }
}