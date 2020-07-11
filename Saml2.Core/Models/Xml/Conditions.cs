using Saml2.Core.Constants;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Conditions
    {
        [XmlAttribute(DataType = "dateTime", AttributeName = "NotBefore")]
        public DateTime NotBefore { get; set; }

        [XmlAttribute(DataType = "dateTime", AttributeName = "NotOnOrAfter")]
        public DateTime NotOnOrAfter { get; set; }

        [XmlElement(ElementName = "AudienceRestriction", Namespace = NamespaceConstant.Saml)]
        public List<AudienceRestriction> AudienceRestrictions { get; set; }

        [XmlElement("OneTimeUse", Namespace = NamespaceConstant.Saml)]
        public OneTimeUse OneTimeUse { get; set; }

        [XmlElement("ProxyRestriction", Namespace = NamespaceConstant.Saml)]
        public ProxyRestriction ProxyRestriction { get; set; }
    }
}