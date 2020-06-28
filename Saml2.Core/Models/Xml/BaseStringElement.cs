using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseStringElement
    {
        [XmlText]
        public string Value { get; set; }
    }
}