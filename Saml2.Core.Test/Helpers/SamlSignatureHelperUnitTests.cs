using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Saml2.Core.Factories;
using Saml2.Core.Helpers;
using Saml2.Core.Providers;
using Xunit;

namespace Saml2.Core.Test.Helpers
{
    public class SamlSignatureHelperUnitTests
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly ISamlAsymmetricKeyProvider asymmetricKeyProvider;
        private readonly IIdpConfigurationProviderFactory idpConfigurationProviderFactory;
        private readonly IIdpConfigurationProvider idpConfigurationProvider;
        private readonly ISamlSignatureHelper helper;

        public SamlSignatureHelperUnitTests()
        {
            this.spConfigurationProvider = Substitute.For<ISpConfigurationProvider>();
            this.asymmetricKeyProvider = Substitute.For<ISamlAsymmetricKeyProvider>();
            this.idpConfigurationProviderFactory = Substitute.For<IIdpConfigurationProviderFactory>();
            this.idpConfigurationProvider = Substitute.For<IIdpConfigurationProvider>();
            this.idpConfigurationProviderFactory.Get().Returns(this.idpConfigurationProvider);


            this.helper = new SamlSignatureHelper(
                this.spConfigurationProvider,
                this.idpConfigurationProviderFactory,
                this.asymmetricKeyProvider
            );
        }

        [Fact]
        public void ShouldValidateAssertionSignature()
        {
            #region Arrange
            string response = FileHelper.Read("./TestData/SapIdpEncodedResponse.txt");
            byte[] decodedBytes = Convert.FromBase64String(response);
            string decodedResponse = Encoding.UTF8.GetString(decodedBytes);

            string cert = "MIIDGjCCAgKgAwIBAQIGAWdjbxc7MA0GCSqGSIb3DQEBBQUAMEwxCzAJBgNVBAYTAkRFMQ8wDQYDVQQKEwZTQVAtU0UxLDAqBgNVBAMTI3NjLXFhLTAwMDEuYWNjb3VudHM0MDAub25kZW1hbmQuY29tMB4XDTE4MTEzMDA3MDQ0NFoXDTI4MTEzMDA3MDQ0NFowTDELMAkGA1UEBhMCREUxDzANBgNVBAoTBlNBUC1TRTEsMCoGA1UEAxMjc2MtcWEtMDAwMS5hY2NvdW50czQwMC5vbmRlbWFuZC5jb20wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCTg3g/bwtfn1HO2mqjMAQhb0C1Vl+pDvVPl7aqtzikMArOj0g7wVCdrYpBCTngxw3wb7S7QhAi92TO2B73S/59JGKg8TQFTfT7lV9dEZ/ilAML+XL22atnB5zEC5QCtMooTcfw/HLGwYhA6bbisgSIimZ9Ju4Oy3KvsoQ70rqzpryltRD3F+1hJqDW8vKlekFU2TPy0gj2EuKUFxxwQkBe0rlMaHScK9Un7SKklIHdvA1B5oKfbAsk9oV+OrzCQ88GScP7sTL/9v9azCgiu3NjFOgEDynNxwBTlRC7ttspfwWVz0wnnEfzjbgwewRS1MvP6urw7md3iStSZnjHUDNLAgMBAAGCAgAAMA0GCSqGSIb3DQEBBQUAA4IBAQCJzi6GZ2DMOm30AZvK022LAOpfMc+NKWAhMohe6bkDzTJT6q7iisMY9joUq8etuVF6cieWUqKehSHXQNyNaaENi7JmaRNglcya65yB9QxaWjKhQSyOogIpng3IKRavyuc/GWX5+PGolUrsxtinBewP8Ah0EgbmIX/yTGx3j4/SKaxzVn2tzqmurfEXR8J1pFaDwQ6Bgn6oRVgReMYp++5/AFqAocBoVVK/Qt70rb1iDWDjWCu70qhL5XvPbZW2W/o+Jw0o7YJ/DkKHKaZB8QFRxj4Txa7NOd8QUYjMTF2ImQhxjfowaajvcCjg6AjMN+zQPdJJhV3xX/31p/VTtmyk";

            this.configureX509FromCertString(cert);

            #endregion

            #region Act

            bool result = this.helper.VerifyXmlSignature(decodedResponse);

            #endregion

            #region Assert

            Assert.True(result);

            #endregion   
        }


        [Fact]
        public void ShouldValidateResponseSignature_2()
        {
            #region Arrange
            string response = FileHelper.Read("./TestData/OktaEncodedResponse.txt");
            byte[] decodedBytes = Convert.FromBase64String(response);
            string decodedResponse = Encoding.UTF8.GetString(decodedBytes);

            string cert = "MIIDpjCCAo6gAwIBAgIGAXY8Svv+MA0GCSqGSIb3DQEBCwUAMIGTMQswCQYDVQQGEwJVUzETMBEGA1UECAwKQ2FsaWZvcm5pYTEWMBQGA1UEBwwNU2FuIEZyYW5jaXNjbzENMAsGA1UECgwET2t0YTEUMBIGA1UECwwLU1NPUHJvdmlkZXIxFDASBgNVBAMMC2Rldi03Mzc1MTAxMRwwGgYJKoZIhvcNAQkBFg1pbmZvQG9rdGEuY29tMB4XDTIwMTIwNzA4MjA1NVoXDTMwMTIwNzA4MjE1NVowgZMxCzAJBgNVBAYTAlVTMRMwEQYDVQQIDApDYWxpZm9ybmlhMRYwFAYDVQQHDA1TYW4gRnJhbmNpc2NvMQ0wCwYDVQQKDARPa3RhMRQwEgYDVQQLDAtTU09Qcm92aWRlcjEUMBIGA1UEAwwLZGV2LTczNzUxMDExHDAaBgkqhkiG9w0BCQEWDWluZm9Ab2t0YS5jb20wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCLSuz4rJlBkR0yvzaApASmKPNuTa73+KfcNznMWQ/t092aD70S6cSskC+pR20xxLIftR/a4IPtX2e9ooA1fW1ldgZJl8JdX2RWQSr9stKtEv7xQ//Td1Lmo7p/3ZQQP+XF9hD/RnDbuL9H57Jj65ApTn0BKmMlH2JGNxKkPCMq+sDSKPehTZyh1pXiyfrLULxatVuvowO5mputCoe2ni67COtBQPGNuTX5SC8u2EaNLR/n0iPrFtpUh/38yfA5DLcVu4M1yWqqaypTl7xV01Xb/UGR7JONpF+QAcvgWTylAZgc2YwdxO75NBbV1jI6qnVC84ce0FwPmTf5yPZv9+a9AgMBAAEwDQYJKoZIhvcNAQELBQADggEBACYZmOAUg3sLRnDpWAuie7GqHme0ruBUym4McjypFoXEpVYlzwe/NENKD1DVaYS/kdBAL3PgWoO2vWiSoZDv7PtnWRZK0LoovUEBxTj9K1uks9ys3e1eWZgWAN9ZYrX4DzVDuOWKeBWMAhAZmWmc4TwihFF+sq3lxrzYJUZZaoe7AWWHgGT1kEzcJvaU22vWD8L9ewnij6iOoGnDyHz59u8heZk7b//BaEmLlHAiJVyFU8f+LxDvUfh6EMKwAhRmKfmjpbXfbpbo8eV05TK9ak0zI1q2pf9qJf8JHQBNnTex1IwOtOlV5h+2a74eCB7KUgKvQ+bomwnBGqFA8tgpE4Y=";

            this.configureX509FromCertString(cert);

            #endregion

            #region Act

            bool result = this.helper.VerifyXmlSignature(decodedResponse);

            #endregion

            #region Assert

            Assert.True(result);

            #endregion  
        }



        private void configureX509FromCertString(string cert)
        {
            idpConfigurationProvider.GetPublicKey().Returns(cert);

            byte[] publicKeyBytes = Convert.FromBase64String(cert);
            X509Certificate2 x509 = new X509Certificate2(publicKeyBytes);
            asymmetricKeyProvider.GetPublicX509(cert).Returns(x509);
        }
    }
}
