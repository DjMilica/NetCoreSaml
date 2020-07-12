using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlRoot(ElementName = SamlElementSelector.AuthnResponse, Namespace = NamespaceConstant.Samlp )]
    public class AuthnResponse: BaseStatusResponse
    {
        [XmlElement(ElementName = SamlElementSelector.Assertion, Namespace = NamespaceConstant.Saml)]
        public List<Assertion> Assertions { get; set; }

        [XmlElement(ElementName = SamlElementSelector.EncryptedAssertion, Namespace = NamespaceConstant.Saml)]
        public List<EncryptedAssertion> EncryptedAssertions { get; set; }
    }
}
