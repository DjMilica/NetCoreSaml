using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseNameId: BaseStringElement
    {
        [XmlAttribute(DataType = "string", AttributeName = "NameQualifier")]
        public string NameQualifier { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "SPNameQualifier")]
        public string SpNameQualifier { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "Format")]
        public string Format { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "SPProvidedID")]
        public string SpProvidedId { get; set; }
    }
}
