using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptedData: BaseEncryptedType
    {
        [XmlElement(ElementName = "KeyInfo", Namespace = NamespaceConstant.Dsig)]
        public KeyInfo KeyInfo { get; set; }
    }
}