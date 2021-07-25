using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Factories;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Saml2.Core.Helpers
{
    public interface ISamlSignatureHelper
    {
        string Build(SignatureAlgorithm signatureAlgorithm, string dataToSign);
        bool VerifyXmlSignature(string xml);
        bool VerifyXmlSignatureWithPublicKey(string xml, string publicKey);
    }

    public class SamlSignatureHelper: ISamlSignatureHelper
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly ISamlAsymmetricKeyProvider asymmetricKeyProvider;
        private readonly IIdpConfigurationProviderFactory idpConfigurationProviderFactory;

        public SamlSignatureHelper(
            ISpConfigurationProvider spConfigurationProvider,
            IIdpConfigurationProviderFactory idpConfigurationProviderFactory,
            ISamlAsymmetricKeyProvider asymmetricKeyProvider
        )
        {
            this.spConfigurationProvider = spConfigurationProvider;
            this.idpConfigurationProviderFactory = idpConfigurationProviderFactory;
            this.asymmetricKeyProvider = asymmetricKeyProvider;
        }

        public string Build(SignatureAlgorithm signatureAlgorithm, string dataToSign)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataToSign);

            string privateKeyString = this.spConfigurationProvider.GetPrivateKey();

            RSA rsa = this.asymmetricKeyProvider.GetPrivateRSA(privateKeyString);

            byte[] signature = rsa.SignData(data, signatureAlgorithm.HashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signature);
        }

        public bool VerifyXmlSignature(string xml)
        {
            IIdpConfigurationProvider idpConfigurationProvider = this.idpConfigurationProviderFactory.Get();
            string idpPublicKey = idpConfigurationProvider.GetPublicKey();

            return this.VerifyXmlSignatureWithPublicKey(xml, idpPublicKey);
        }

        public bool VerifyXmlSignatureWithPublicKey(string xml, string publicKey)
        {
            X509Certificate2 cert = this.asymmetricKeyProvider.GetPublicX509(publicKey);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            SignedXml signedXml = new SignedXml(xmlDocument);

            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("ds:Signature");

            signedXml.LoadXml((XmlElement)nodeList[0]);

            return signedXml.CheckSignature(cert, true);
        }
    }
}
