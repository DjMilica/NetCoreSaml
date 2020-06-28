using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptionProperty: BaseStringElement
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlAttribute(DataType = "anyURI", AttributeName = "Target")]
        public string Target { get; set; }

        [XmlAnyAttribute]
        public string[] OtherAttributes { get; set; }
    }
}