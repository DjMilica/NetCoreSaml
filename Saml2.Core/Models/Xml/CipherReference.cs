using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class CipherReference
    {
        [XmlElement("Transforms")]
        public Transforms Transforms { get; set; }
    }
}