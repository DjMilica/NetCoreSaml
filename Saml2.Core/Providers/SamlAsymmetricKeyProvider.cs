using Saml2.Core.Errors;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Saml2.Core.Providers
{
    public interface ISamlAsymmetricKeyProvider
    {
        RSA GetPrivateRSA(string key);
        X509Certificate2 GetPublicX509(string key);
    }

    public class SamlAsymmetricKeyProvider : ISamlAsymmetricKeyProvider
    {
        public RSA GetPrivateRSA(string key)
        {
            RSA rsa = RSA.Create();

            bool isPkcsprivateKey = key.Contains("BEGIN PRIVATE KEY");

            string transformedKey = key.Replace("-----BEGIN PRIVATE KEY-----", string.Empty).Replace("-----END PRIVATE KEY-----", string.Empty);
            transformedKey = transformedKey.Replace("-----BEGIN RSA PRIVATE KEY-----", string.Empty).Replace("-----END RSA PRIVATE KEY-----", string.Empty);
            transformedKey = transformedKey.Replace(Environment.NewLine, string.Empty);
            byte[] privateKeyBytes = Convert.FromBase64String(transformedKey);

            if (isPkcsprivateKey)
            {
                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out int _);
            }
            else
            {
                rsa.ImportRSAPrivateKey(privateKeyBytes, out int _);
            }

            return rsa;
        }

        public X509Certificate2 GetPublicX509(string key)
        {
            string transformedKey = key.Replace("-----BEGIN CERTIFICATE-----", string.Empty).Replace("-----END CERTIFICATE-----", string.Empty);
            transformedKey = transformedKey.Replace(Environment.NewLine, string.Empty);
            byte[] publicKeyBytes = Convert.FromBase64String(transformedKey);

            return new X509Certificate2(publicKeyBytes);
        }
    }
}
