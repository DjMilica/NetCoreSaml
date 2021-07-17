using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Saml2.Core.Helpers
{
    public interface ISamlSignatureHelper
    {
        string Build(SignatureAlgorithm signatureAlgorithm, string dataToSign);
    }

    public class SamlSignatureHelper: ISamlSignatureHelper
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly IRsaKeyProvider rsaKeyProvider;

        public SamlSignatureHelper(
            ISpConfigurationProvider spConfigurationProvider,
            IRsaKeyProvider rsaKeyProvider
        )
        {
            this.spConfigurationProvider = spConfigurationProvider;
            this.rsaKeyProvider = rsaKeyProvider;
        }

        public string Build(SignatureAlgorithm signatureAlgorithm, string dataToSign)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataToSign);

            string privateKeyString = this.spConfigurationProvider.GetPrivateKey();

            RSA rsa = this.rsaKeyProvider.GetPrivate(privateKeyString);

            byte[] signature = rsa.SignData(data, signatureAlgorithm.HashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signature);
        }
    }
}
