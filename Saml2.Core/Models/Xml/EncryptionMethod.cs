using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptionMethod
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = "Algorithm")]
        public string Algorithm { get; set; }

        [XmlElement("KeySize")]
        public KeySize KeySize { get; set; }

        [XmlElement("OAEPparams")]
        public OaepParams OaepParams { get; set; }

        [XmlAnyElement]
        public object[] OtherElements { get; set; }
    }
}