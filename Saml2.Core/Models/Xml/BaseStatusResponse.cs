using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseStatusResponse: BaseCorrespondance
    {
        [XmlAttribute(DataType = "NCName", AttributeName = "InResponseTo")]
        public string InResponseTo { get; set; }

        [XmlElement(ElementName = "Status", Namespace = NamespaceConstant.Samlp)]
        public Status Status { get; set; }
    }
}
