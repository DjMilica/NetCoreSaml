using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class CipherData
    {
        [XmlElement("CipherValue")]
        public CipherValue CipherValue { get; set; }

        [XmlElement("CipherReference")]
        public CipherReference CipherReference { get; set; }
    }
}