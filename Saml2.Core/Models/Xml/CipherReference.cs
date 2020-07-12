using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class CipherReference
    {
        [XmlElement(ElementName = SamlElementSelector.Transforms, Namespace = NamespaceConstant.Xenc)]
        public Transforms Transforms { get; set; }
    }
}