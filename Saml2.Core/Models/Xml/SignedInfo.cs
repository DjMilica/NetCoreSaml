using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SignedInfo
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdLowerCase)]
        public string Id { get; set; }

        [XmlElement(ElementName = SamlElementSelector.CanonicalizationMethod, Namespace = NamespaceConstant.Dsig)]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }

        [XmlElement(ElementName = SamlElementSelector.SignatureMethod, Namespace = NamespaceConstant.Dsig)]
        public SignatureMethod SignatureMethod { get; set; }

        [XmlElement(ElementName = SamlElementSelector.Reference, Namespace = NamespaceConstant.Dsig)]
        public List<Reference> References { get; set; }
    }
}