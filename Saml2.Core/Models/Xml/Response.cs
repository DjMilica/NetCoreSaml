using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlRoot("Response")]
    public class Response: BaseStatusResponse
    {
        [XmlElement("Assertion")]
        public List<Assertion> Assertions { get; set; }

        [XmlElement("EncryptedAssertion")]
        public List<EncryptedAssertion> EncryptedAssertions { get; set; }
    }
}
