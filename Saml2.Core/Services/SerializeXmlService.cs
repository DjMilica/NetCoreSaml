using System.IO;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace Saml2.Core.Services
{
    public interface ISerializeXmlService
    {
        T Deserialize<T>(string data) where T : class;
        string Serialize<T>(T data) where T : class;
    }

    public class SerializeXmlService : ISerializeXmlService
    {
        private readonly ILogger<ISerializeXmlService> logger;

        public SerializeXmlService(
            ILogger<ISerializeXmlService> logger
        )
        {
            this.logger = logger;
        }

        public T Deserialize<T>(string data) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.UnknownElement += new XmlElementEventHandler(this.UnknownElementHandler);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(this.UnknownAttributeHandler);

            using (TextReader reader = new StringReader(data))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public string Serialize<T>(T data) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, data);
                return writer.ToString();
            }
        }

        private void UnknownElementHandler(object sender, XmlElementEventArgs e)
        {
            this.logger.LogInformation($"Unknown element found serializing xml - {e.Element.Name} with value {e.Element.InnerText}.");
        }

        private void UnknownAttributeHandler(object sender, XmlAttributeEventArgs e)
        {
            this.logger.LogInformation($"Unknown attribute found serializing xml - {e.Attr.Name} with value {e.Attr.Value}.");
        }
    }
}
