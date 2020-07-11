﻿using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class EncryptionProperties
    {
        [XmlAttribute(DataType = "ID", AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "EncryptionProperty", Namespace = NamespaceConstant.Xenc)]
        public List<EncryptionProperty> EncryptionPropertyList { get; set; }
    }
}