using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Transforms
    {
        [XmlElement(ElementName = SamlElementSelector.Transform, Namespace = NamespaceConstant.Dsig)]
        public List<Transform> TransformList { get; set; }
    }
}