using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Transforms
    {
        [XmlElement("Transform")]
        public List<Transform> TransformList { get; set; }
    }
}