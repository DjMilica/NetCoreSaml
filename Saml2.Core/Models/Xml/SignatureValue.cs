using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SignatureValue: Base64BinaryElement
    {
        [XmlAttribute(DataType = "ID", AttributeName = SamlAttributeSelector.IdLowerCase)]
        public string Id { get; set; }
    }
}