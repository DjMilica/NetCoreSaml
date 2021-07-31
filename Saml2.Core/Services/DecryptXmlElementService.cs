using Saml2.Core.Helpers;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Saml2.Core.Services
{
    public interface IDecryptXmlElementService
    {
        List<T> DecryptElementsFromXml<T>(string xml, string encryptedElementName) where T : class;
        T DecryptElementFromParsedXml<T, M>(M encElement, string encryptedElementName) where T : class where M : BaseEncryptedElement;
    }

    public class DecryptXmlElementService: IDecryptXmlElementService
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly ISamlAsymmetricKeyProvider asymmetricKeyProvider;
        private readonly ISerializeXmlService serializeXmlService;
        private readonly ISamlEncryptionHelper samlEncryptionHelper;

        public DecryptXmlElementService(
            ISpConfigurationProvider spConfigurationProvider,
            ISamlAsymmetricKeyProvider asymmetricKeyProvider,
            ISerializeXmlService serializeXmlService,
            ISamlEncryptionHelper samlEncryptionHelper
        )
        {
            this.spConfigurationProvider = spConfigurationProvider;
            this.asymmetricKeyProvider = asymmetricKeyProvider;
            this.serializeXmlService = serializeXmlService;
            this.samlEncryptionHelper = samlEncryptionHelper;
        }

        public List<T> DecryptElementsFromXml<T>(string xml, string encryptedElementName) where T : class
        {
            RSA rsaPrivateKey = this.GetRsaPrivateKey();

            List<T> decryptedElements = new List<T>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNodeList encryptedElems = xmlDoc.GetElementsByTagName(encryptedElementName);

            for (int i = 0; i < encryptedElems.Count; i++)
            {
                XmlElement encryptedElement = encryptedElems[i] as XmlElement;

                T parsedElement = this.samlEncryptionHelper.DecryptXmlElement<T>(encryptedElement, rsaPrivateKey);

                decryptedElements.Add(parsedElement);
            }

            return decryptedElements;
        }

        public T DecryptElementFromParsedXml<T, M>(M encElement, string encryptedElementName) where T : class where M : BaseEncryptedElement
        {
            RSA rsaPrivateKey = this.GetRsaPrivateKey();

            string encryptedElementXml = this.serializeXmlService.Serialize<M>(encElement);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(encryptedElementXml);

            XmlNodeList encryptedElems = xmlDoc.GetElementsByTagName(encryptedElementName);

            XmlElement encryptedElement = encryptedElems[0] as XmlElement;

            T parsedElement = this.samlEncryptionHelper.DecryptXmlElement<T>(encryptedElement, rsaPrivateKey);

            return parsedElement;
        }

        private RSA GetRsaPrivateKey()
        {
            string spPrivateKeyString = this.spConfigurationProvider.GetPrivateKey();

            RSA rsaPrivateKey = this.asymmetricKeyProvider.GetPrivateRSA(spPrivateKeyString);

            return rsaPrivateKey;
        }
    }
}
