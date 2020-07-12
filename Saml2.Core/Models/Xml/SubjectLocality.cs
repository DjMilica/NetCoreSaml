using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class SubjectLocality
    {
        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.Address)]
        public string Address { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.DnsName)]
        public string DnsName { get; set; }
    }
}