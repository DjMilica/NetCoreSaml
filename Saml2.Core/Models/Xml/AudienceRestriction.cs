using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class AudienceRestriction
    {
        [XmlElement(ElementName = "Audience", Namespace = NamespaceConstant.Saml)]
        public List<Audience> Audiences { get; set; }
    }
}