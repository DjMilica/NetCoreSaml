using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Reference
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdLowerCase)]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Uri)]
        public string Uri { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Type)]
        public string Type { get; set; }

        [XmlElement(ElementName = SamlElementSelector.Transforms, Namespace = NamespaceConstant.Dsig)]
        public Transforms Transforms { get; set; }

        [XmlElement(ElementName = SamlElementSelector.DigestMethod, Namespace = NamespaceConstant.Dsig)]
        public DigestMethod DigestMethod { get; set; }

        [XmlElement(ElementName = SamlElementSelector.DigestValue, Namespace = NamespaceConstant.Dsig)]
        public DigestValue DigestValue { get; set; }
    }
}