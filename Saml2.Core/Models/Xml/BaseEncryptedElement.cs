﻿using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public abstract class BaseEncryptedElement
    {
        [XmlElement(ElementName = "EncryptedData", Namespace = NamespaceConstant.Xenc)]
        public EncryptedData EncryptedData { get; set; }

        [XmlElement(ElementName = "EncryptedKey", Namespace = NamespaceConstant.Xenc)]
        public List<EncryptedKey> EncryptedKey { get; set; }
    }
}
