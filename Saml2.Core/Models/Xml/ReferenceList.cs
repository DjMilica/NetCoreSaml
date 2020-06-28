using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class ReferenceList
    {
        [XmlElement("DataReference")]
        public List<DataReference> DataReferences { get; set; }

        [XmlElement("KeyReference")]
        public List<KeyReference> KeyReferences { get; set; }
    }
}