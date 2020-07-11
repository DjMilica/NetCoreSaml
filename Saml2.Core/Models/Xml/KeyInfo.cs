﻿using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class KeyInfo
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "KeyName", Namespace = NamespaceConstant.Dsig)]
        public KeyName KeyName { get; set; }

        [XmlElement(ElementName = "X509Data", Namespace = NamespaceConstant.Dsig)]
        public X509Data X509Data { get; set; }

        [XmlElement(ElementName = "EncryptedKey", Namespace = NamespaceConstant.Xenc)]
        public List<EncryptedKey> EncryptedKeys { get; set; }
    }
}