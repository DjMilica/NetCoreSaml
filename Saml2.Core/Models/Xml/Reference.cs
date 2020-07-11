using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Reference
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "URI")]
        public string Uri { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "Type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "Transforms", Namespace = NamespaceConstant.Dsig)]
        public Transforms Transforms { get; set; }

        [XmlElement(ElementName = "DigestMethod", Namespace = NamespaceConstant.Dsig)]
        public DigestMethod DigestMethod { get; set; }

        [XmlElement(ElementName = "DigestValue", Namespace = NamespaceConstant.Dsig)]
        public DigestValue DigestValue { get; set; }
    }
}