using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptionProperty: BaseStringElement
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdLowerCase)]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Target)]
        public string Target { get; set; }

        [XmlAnyAttribute]
        public string[] OtherAttributes { get; set; }
    }
}