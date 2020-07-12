using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Status
    {
        [XmlElement(ElementName = SamlElementSelector.StatusCode, Namespace = NamespaceConstant.Samlp)]
        public StatusCode StatusCode { get; set; }

        [XmlElement(ElementName = SamlElementSelector.StatusMessage, Namespace = NamespaceConstant.Samlp)]
        public StatusMessage StatusMessage { get; set; }

        [XmlElement(ElementName = SamlElementSelector.StatusDetail, Namespace = NamespaceConstant.Samlp)]
        public StatusDetail StatusDetail { get; set; }
    }
}
