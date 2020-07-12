using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptionMethod
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Algorithm)]
        public string Algorithm { get; set; }

        [XmlElement(ElementName = SamlElementSelector.KeySize, Namespace = NamespaceConstant.Xenc)]
        public KeySize KeySize { get; set; }

        [XmlElement(ElementName = SamlElementSelector.OaepParams, Namespace = NamespaceConstant.Xenc)]
        public OaepParams OaepParams { get; set; }

        [XmlAnyElement]
        public object[] OtherElements { get; set; }
    }
}