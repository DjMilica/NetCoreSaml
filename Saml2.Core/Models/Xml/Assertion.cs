using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Assertion: BaseRootElement
    {
        [XmlElement("Subject")]
        public Subject Subject { get; set; }

        [XmlElement("Conditions")]
        public Conditions Condition { get; set; }

        [XmlElement("AuthnStatement")]
        public List<AuthnStatement> AuthnStatements { get; set; }

        [XmlElement("AttributeStatement")]
        public List<AttributeStatement> AttributeStatements { get; set; }
    }
}
