using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class ReferenceList
    {
        [XmlElement(ElementName = "DataReference", Namespace = NamespaceConstant.Xenc)]
        public List<DataReference> DataReferences { get; set; }

        [XmlElement(ElementName = "KeyReference", Namespace = NamespaceConstant.Xenc)]
        public List<KeyReference> KeyReferences { get; set; }
    }
}