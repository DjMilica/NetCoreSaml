using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using Saml2.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Saml2.Core.Helpers
{
    public interface ISamlEncryptionHelper
    {
        T DecryptXmlElement<T>(XmlElement encryptedElement, RSA rsaPrivateKey) where T : class;
        byte[] RsaDecryptKey(XmlElement encryptedElement, RSA rsaPrivateKey);
        string SymmetricDecryptElement(XmlElement encryptedElement, byte[] decryptedKey);
    }

    public class SamlEncryptionHelper: ISamlEncryptionHelper
    {
        private readonly ISerializeXmlService serializeXmlService;

        public SamlEncryptionHelper(
            ISerializeXmlService serializeXmlService
        )
        {
            this.serializeXmlService = serializeXmlService;
        }

        public T DecryptXmlElement<T>(XmlElement encryptedElement, RSA rsaPrivateKey) where T: class
        {
            byte[] decryptedKey = this.RsaDecryptKey(encryptedElement, rsaPrivateKey);

            string decryptedElement = this.SymmetricDecryptElement(encryptedElement, decryptedKey);

            T parsedElement = this.serializeXmlService.Deserialize<T>(decryptedElement);

            return parsedElement;
        }

        public byte[] RsaDecryptKey(XmlElement encryptedElement, RSA rsaPrivateKey)
        {
            System.Security.Cryptography.Xml.EncryptedKey encryptedKey;

            XmlElement encryptedDataElement = encryptedElement.GetElementsByTagName("EncryptedData", NamespaceConstant.Xenc)[0] as XmlElement;
            System.Security.Cryptography.Xml.EncryptedData encryptedData = new System.Security.Cryptography.Xml.EncryptedData();
            encryptedData.LoadXml(encryptedDataElement);

            List<KeyInfoEncryptedKey> keyInfos = encryptedData.KeyInfo.OfType<KeyInfoEncryptedKey>().ToList();

            encryptedKey = keyInfos.First()?.EncryptedKey;

            if (encryptedKey == null)
            {
                XmlElement encryptedKeyElement = encryptedElement.GetElementsByTagName("EncryptedKey", NamespaceConstant.Xenc)[0] as XmlElement;

                SamlValidationGuard.NotNull(
                    encryptedKeyElement,
                    "Key not present in SAML encrypted element."
                );

                encryptedKey = new System.Security.Cryptography.Xml.EncryptedKey();
                encryptedKey.LoadXml(encryptedElement);
            }

            byte[] encKeyBytes = encryptedKey.CipherData.CipherValue;
            string encKeyAlgorithm = encryptedKey.EncryptionMethod.KeyAlgorithm;

            bool useOaep = encKeyAlgorithm == EncryptionAlgorithm.RsaOaep || encKeyAlgorithm == EncryptionAlgorithm.RsaOaepMgf1p;

            byte[] decryptedKey = EncryptedXml.DecryptKey(encKeyBytes, rsaPrivateKey, useOaep);

            return decryptedKey;
        }

        public string SymmetricDecryptElement(XmlElement encryptedElement, byte[] decryptedKey)
        {
            XmlElement xmlEncryptedDataElement = encryptedElement.GetElementsByTagName("EncryptedData", NamespaceConstant.Xenc)[0] as XmlElement;

            System.Security.Cryptography.Xml.EncryptedData encryptedData = new System.Security.Cryptography.Xml.EncryptedData();
            encryptedData.LoadXml(xmlEncryptedDataElement);

            EncryptedXml exml = new EncryptedXml();

            using Aes aes = Aes.Create();
            aes.Key = decryptedKey;
            aes.IV = exml.GetDecryptionIV(encryptedData, encryptedData.EncryptionMethod.KeyAlgorithm);
            aes.Padding = PaddingMode.None;

            byte[] decryptedBytes = exml.DecryptData(encryptedData, aes);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
