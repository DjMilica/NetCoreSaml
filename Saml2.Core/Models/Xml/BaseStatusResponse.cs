using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseStatusResponse: BaseCorrespondance
    {
        [XmlAttribute(DataType = "NCName", AttributeName = "InResponseTo")]
        public string InResponseTo { get; set; }

        [XmlElement("Status")]
        public Status Status { get; set; }
    }
}
