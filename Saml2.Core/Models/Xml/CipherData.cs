﻿using Saml2.Core.Constants;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class CipherData
    {
        [XmlElement(ElementName = "CipherValue", Namespace = NamespaceConstant.Xenc)]
        public CipherValue CipherValue { get; set; }

        [XmlElement(ElementName = "CipherReference", Namespace = NamespaceConstant.Xenc)]
        public CipherReference CipherReference { get; set; }
    }
}