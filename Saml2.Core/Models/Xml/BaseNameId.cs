using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseNameId: BaseStringElement
    {
        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.NameQualifier)]
        public string NameQualifier { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.SpNameQualifier)]
        public string SpNameQualifier { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Format)]
        public string Format { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.SpProvidedId)]
        public string SpProvidedId { get; set; }
    }
}
