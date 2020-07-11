using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlRoot(ElementName = "Response", Namespace = NamespaceConstant.Samlp )]
    public class Response: BaseStatusResponse
    {
        [XmlElement(ElementName = "Assertion", Namespace = NamespaceConstant.Saml)]
        public List<Assertion> Assertions { get; set; }

        [XmlElement(ElementName = "EncryptedAssertion", Namespace = NamespaceConstant.Saml)]
        public List<EncryptedAssertion> EncryptedAssertions { get; set; }
    }
}
