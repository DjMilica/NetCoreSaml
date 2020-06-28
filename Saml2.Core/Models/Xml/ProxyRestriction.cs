using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class ProxyRestriction
    {
        [XmlAttribute(DataType = "nonNegativeInteger", AttributeName = "Count")]
        public string Count { get; set; }

        [XmlElement("Audience")]
        public List<Audience> Audiences { get; set; }
    }
}