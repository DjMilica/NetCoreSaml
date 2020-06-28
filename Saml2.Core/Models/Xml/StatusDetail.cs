using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class StatusDetail
    {
        [XmlAnyElement]
        public object[] Details { get; set; }
    }
}