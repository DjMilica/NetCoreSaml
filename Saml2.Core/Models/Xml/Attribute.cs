using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Attribute
    {
        [XmlAttribute(DataType = "string", AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "NameFormat")]
        public string NameFormat { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "FriendlyName")]
        public string FriendlyName { get; set; }

        [XmlElement("AttributeValue")]
        public List<AttributeValue> AttributeValues { get; set; }
    }
}