using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class NameIdPolicy
    {
        [XmlAttribute(DataType = "anyURI", AttributeName = SamlAttributeSelector.Format)]
        public string Format { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.SpNameQualifier)]
        public string SpNameQualifier { get; set; }

        [XmlAttribute(DataType = "boolean", AttributeName = SamlAttributeSelector.AllowCreate)]
        public bool AllowCreate { get; set; }
    }
}
