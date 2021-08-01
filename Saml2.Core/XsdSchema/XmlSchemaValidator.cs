using Saml2.Core.Errors;
using Saml2.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Saml2.Core.XsdSchema
{
    public interface IXmlSchemaValidator
    {
        void ValidateAuthnResponse(string xml);
    }

    public class XmlSchemaValidator: IXmlSchemaValidator
    {
        public void ValidateAuthnResponse(string xml)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            XmlSchema protocolSchema = new XmlSchema();

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string schemaXml = FileHelper.Read(Path.Combine(executableLocation, $"./XsdSchema/Files/{XsdSchemaFileNameConstant.Protocol}"));
            using MemoryStream xsdMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            protocolSchema.Write(xsdMemoryStream);

            settings.Schemas.Add(protocolSchema);
            settings.ValidationType = ValidationType.Schema;

            using MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlReader reader = XmlReader.Create(memoryStream, settings);
            XmlDocument document = new XmlDocument();
            document.Load(reader);

            ValidationEventHandler eventHandler = new ValidationEventHandler(XmlSchemaValidator.ValidationEventHandler);

            
            document.Validate(eventHandler);
        }

        public static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    throw new SamlValidationException("Xsd schema is not valid!");
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }
}
