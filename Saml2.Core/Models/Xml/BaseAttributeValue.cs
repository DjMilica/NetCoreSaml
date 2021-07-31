using Saml2.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Saml2.Core.Models.Xml
{
    [XmlType("base-attribute-value", Namespace = NamespaceConstant.Xs)]
    [XmlInclude(typeof(StringBaseAttributeValue)),XmlInclude(typeof(DateBaseAttributeValue))]
    public partial class BaseAttributeValue
    {
        [XmlAttribute(DataType = "string", AttributeName = "type", Namespace = NamespaceConstant.Xsi)]
        public string Type { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = SamlAttributeSelector.XsiNil, Namespace = NamespaceConstant.Xsi)]
        public string Nil { get; set; }

        [XmlText]
        public string Value { get; set; }

        public virtual object GetValue() { return Value; }
    }

    [XmlType(Namespace = NamespaceConstant.Xs, TypeName = "string")]
    public class StringBaseAttributeValue : BaseAttributeValue
    {
        public override object GetValue() { return this.Value; }
    }

    [XmlType(Namespace = NamespaceConstant.Xs, TypeName = "date")]
    public class DateBaseAttributeValue : BaseAttributeValue
    {
        public override object GetValue() { return DateTime.Parse(this.Value); }
    }

}
