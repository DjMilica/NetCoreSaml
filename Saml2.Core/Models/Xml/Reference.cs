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

        [XmlElement("Transforms")]
        public Transforms Transforms { get; set; }

        [XmlElement("DigestMethod")]
        public DigestMethod DigestMethod { get; set; }

        [XmlElement("DigestValue")]
        public DigestValue DigestValue { get; set; }
    }
}