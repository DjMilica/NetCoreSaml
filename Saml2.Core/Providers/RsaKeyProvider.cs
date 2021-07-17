using Saml2.Core.Errors;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Saml2.Core.Providers
{
    public interface IRsaKeyProvider
    {
        RSA GetPrivate(string key);
    }

    public class RsaKeyProvider : IRsaKeyProvider
    {
        public RSA GetPrivate(string key)
        {
            RSA rsa = RSA.Create();

            bool isPkcsprivateKey = key.Contains("BEGIN PRIVATE KEY");
            if (isPkcsprivateKey)
            {
                var privateKey = key.Replace("-----BEGIN PRIVATE KEY-----", string.Empty).Replace("-----END PRIVATE KEY-----", string.Empty);
                privateKey = privateKey.Replace(Environment.NewLine, string.Empty);
                var privateKeyBytes = Convert.FromBase64String(privateKey);
                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out int _);
            }
            else
            {
                var privateKey = key.Replace("-----BEGIN RSA PRIVATE KEY-----", string.Empty).Replace("-----END RSA PRIVATE KEY-----", string.Empty);
                privateKey = privateKey.Replace(Environment.NewLine, string.Empty);
                var privateKeyBytes = Convert.FromBase64String(privateKey);
                rsa.ImportRSAPrivateKey(privateKeyBytes, out int _);
            }

            return rsa;
        }
    }
}
