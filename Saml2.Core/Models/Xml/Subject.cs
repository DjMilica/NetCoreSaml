﻿using Saml2.Core.Constants;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    public class Subject
    {
        [XmlElement(ElementName = "NameID", Namespace = NamespaceConstant.Saml)]
        public NameId NameId { get; set; }

        [XmlElement(ElementName = "EncryptedID", Namespace = NamespaceConstant.Saml)]
        public EncryptedId EncryptedId { get; set; }

        [XmlElement(ElementName = "SubjectConfirmation", Namespace = NamespaceConstant.Saml)]
        public List<SubjectConfirmation> SubjectConfirmations { get; set; }
    }
}