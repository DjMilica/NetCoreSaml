using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlRoot(ElementName = SamlElementSelector.NameId)]
    public class NameId: BaseNameId
    {
    }
}