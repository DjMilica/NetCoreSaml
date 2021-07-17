using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Saml2.Core.Constants
{
    public class SignatureAlgorithm
    {
        public string Name;
        public string Url;
        public HashAlgorithmName HashAlgorithmName;

        public SignatureAlgorithm(string name, string url, HashAlgorithmName hashAlgorithmName)
        {
            this.Name = name;
            this.Url = url;
            this.HashAlgorithmName = hashAlgorithmName;
        }
    }

    public class SignatureAlgorithmConstants
    {
        public static SignatureAlgorithm RsaSha256 = new SignatureAlgorithm("RSA-SHA256", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", HashAlgorithmName.SHA256);
        public static SignatureAlgorithm RsaSha1 = new SignatureAlgorithm("RSA-SHA1", "http://www.w3.org/2000/09/xmldsig#rsa-sha1", HashAlgorithmName.SHA1);

    }
}
