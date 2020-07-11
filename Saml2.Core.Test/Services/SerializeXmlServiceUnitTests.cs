﻿using Saml2.Core.Services;
using System;
using NSubstitute;
using Xunit;
using Microsoft.Extensions.Logging;
using Saml2.Core.Models.Xml;

namespace Saml2.Core.Test.Services
{
    public class SerializeXmlServiceUnitTests
    {
        private readonly ILogger<ISerializeXmlService> logger;
        private readonly ISerializeXmlService service;

        public SerializeXmlServiceUnitTests()
        {
            this.logger = Substitute.For<ILogger<ISerializeXmlService>>();

            this.service = new SerializeXmlService(
                this.logger
            );
        }

        [Fact]
        public void ShouldSerializeAuthnResponseAttributes()
        {
            #region Arrange

            string responseWithAttributes = @"<?xml version='1.0' encoding='UTF-8'?>
                <samlp:Response
                     xmlns:samlp = 'urn:oasis:names:tc:SAML:2.0:protocol'
                    ID = '_622edc75-4251-4069-bd0f-cf96a504e83b'
                    Version = '2.0'
                    IssueInstant = '2020-02-28T09:34:06.325Z'
                    Destination = 'https://someSp/start'
                    Consent = 'urn:oasis:names:tc:SAML:2.0:consent:unspecified'
                    InResponseTo = 'ONELOGIN_4fee3b046395c4e751011e97f8900b5273d56685'
                    >
                </samlp:Response >";

            #endregion

            #region Act

            Response result = this.service.Deserialize<Response>(responseWithAttributes);

            #endregion

            #region Assert

            Assert.NotNull(result);
            Assert.Equal("_622edc75-4251-4069-bd0f-cf96a504e83b", result.Id);
            Assert.Equal("2.0", result.Version);
            Assert.Equal(DateTimeOffset.Parse("2020-02-28T09:34:06.325Z") , result.IssueInstant);
            Assert.Equal("https://someSp/start", result.Destination);
            Assert.Equal("ONELOGIN_4fee3b046395c4e751011e97f8900b5273d56685", result.InResponseTo);

            this.logger.Received(1).LogInformation($"Unknown attribute found serializing xml - Consent with value urn:oasis:names:tc:SAML:2.0:consent:unspecified.");

            #endregion  
        }

        [Fact]
        public void ShouldSerializeAuthnResponseSignatureElement()
        {
            #region Arrange

            string x509Cert = "MIICajCCAdOgAwIBAgIBADANBgkqhkiG9w0BAQ0FADBSMQswCQYDVQQGEwJ1czETMBEGA1UECAwKQ2FsaWZvcm5pYTEVMBMGA1UECgwMT25lbG9naW4gSW5jMRcwFQYDVQQDDA5zcC5leGFtcGxlLmNvbTAeFw0xNDA3MTcxNDEyNTZaFw0xNTA3MTcxNDEyNTZaMFIxCzAJBgNVBAYTAnVzMRMwEQYDVQQIDApDYWxpZm9ybmlhMRUwEwYDVQQKDAxPbmVsb2dpbiBJbmMxFzAVBgNVBAMMDnNwLmV4YW1wbGUuY29tMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDZx+ON4IUoIWxgukTb1tOiX3bMYzYQiwWPUNMp+Fq82xoNogso2bykZG0yiJm5o8zv/sd6pGouayMgkx/2FSOdc36T0jGbCHuRSbtia0PEzNIRtmViMrt3AeoWBidRXmZsxCNLwgIV6dn2WpuE5Az0bHgpZnQxTKFek0BMKU/d8wIDAQABo1AwTjAdBgNVHQ4EFgQUGHxYqZYyX7cTxKVODVgZwSTdCnwwHwYDVR0jBBgwFoAUGHxYqZYyX7cTxKVODVgZwSTdCnwwDAYDVR0TBAUwAwEB/zANBgkqhkiG9w0BAQ0FAAOBgQByFOl+hMFICbd3DJfnp2Rgd/dqttsZG/tyhILWvErbio/DEe98mXpowhTkC04ENprOyXi7ZbUqiicF89uAGyt1oqgTUCD1VsLahqIcmrzgumNyTwLGWo17WDAa1/usDhetWAMhgzF/Cnf5ek0nK00m0YZGyc4LzgD0CROMASTWNg==";
            string signatureValue = "d9QXcvDyn+7R8ZMha1W1XcSKctmS+tz5X75ktZRWQ7QHqqkG2h3+wvrtDAIXtzbvolH6+sP0qfcVbBD5XjYuUsqtarnHKmaPwttRKoX2P1tJHKxbpGXbB6e7NoePjqpA211Pjfr0YPrLx2ZfXkkmbYOpDV/yHtg1YwmtJcUo9NY=";

            string responseWithSignature = @$"
                <samlp:Response xmlns:samlp='urn:oasis:names:tc:SAML:2.0:protocol' xmlns:saml='urn:oasis:names:tc:SAML:2.0:assertion' ID='pfx185f69c9-c006-ddeb-ce3b-5f0d0f51a1b2' Version='2.0' IssueInstant='2014-07-17T01:01:48Z' Destination='http://sp.example.com/demo1/index.php?acs' InResponseTo='ONELOGIN_4fee3b046395c4e751011e97f8900b5273d56685'>
                    <ds:Signature xmlns:ds='http://www.w3.org/2000/09/xmldsig#'>
                        <ds:SignedInfo>
                            <ds:CanonicalizationMethod Algorithm='http://www.w3.org/2001/10/xml-exc-c14n#'/>
                            <ds:SignatureMethod Algorithm='http://www.w3.org/2000/09/xmldsig#rsa-sha1'/>
                            <ds:Reference URI='#pfx185f69c9-c006-ddeb-ce3b-5f0d0f51a1b2'>
                                <ds:Transforms>
                                    <ds:Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature'/>
                                    <ds:Transform Algorithm='http://www.w3.org/2001/10/xml-exc-c14n#'/>
                                </ds:Transforms>
                                <ds:DigestMethod Algorithm='http://www.w3.org/2000/09/xmldsig#sha1'/>
                                <ds:DigestValue>Ldgd30/+CCun6XlBOeiJUnpgPJo=</ds:DigestValue>
                            </ds:Reference>
                        </ds:SignedInfo>
                        <ds:SignatureValue>{signatureValue}</ds:SignatureValue>
                        <ds:KeyInfo>
                            <ds:X509Data>
                                <ds:X509Certificate>{x509Cert}</ds:X509Certificate>
                            </ds:X509Data>
                        </ds:KeyInfo>
                    </ds:Signature>
                </samlp:Response>";

            #endregion

            #region Act

            Response result = this.service.Deserialize<Response>(responseWithSignature);

            #endregion

            #region Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Signature);
            Assert.Equal("http://www.w3.org/2001/10/xml-exc-c14n#", result.Signature.SignedInfo.CanonicalizationMethod.Algorithm);
            Assert.Equal("http://www.w3.org/2000/09/xmldsig#rsa-sha1", result.Signature.SignedInfo.SignatureMethod.Algorithm);
            Assert.Equal("#pfx185f69c9-c006-ddeb-ce3b-5f0d0f51a1b2", result.Signature.SignedInfo.References[0].Uri);
            Assert.Equal("http://www.w3.org/2000/09/xmldsig#enveloped-signature", result.Signature.SignedInfo.References[0].Transforms.TransformList[0].Algorithm);
            Assert.Equal("http://www.w3.org/2001/10/xml-exc-c14n#", result.Signature.SignedInfo.References[0].Transforms.TransformList[1].Algorithm);
            Assert.Equal("http://www.w3.org/2000/09/xmldsig#sha1", result.Signature.SignedInfo.References[0].DigestMethod.Algorithm);
            Assert.Equal(Convert.FromBase64String("Ldgd30/+CCun6XlBOeiJUnpgPJo="), result.Signature.SignedInfo.References[0].DigestValue.Value);
            Assert.Equal(Convert.FromBase64String(signatureValue), result.Signature.SignatureValue.Value);
            Assert.Equal(Convert.FromBase64String(x509Cert), result.Signature.KeyInfo.X509Data.X509Certificates[0].Value);

            #endregion  
        }

        [Fact]
        public void ShouldSerializeAuthnResponseIssuerAndStatusElements()
        {
            #region Arrange

            string response = @$"
                <samlp:Response xmlns:samlp='urn:oasis:names:tc:SAML:2.0:protocol' xmlns:saml='urn:oasis:names:tc:SAML:2.0:assertion' ID='pfx185f69c9-c006-ddeb-ce3b-5f0d0f51a1b2' Version='2.0' IssueInstant='2014-07-17T01:01:48Z' Destination='http://sp.example.com/demo1/index.php?acs' InResponseTo='ONELOGIN_4fee3b046395c4e751011e97f8900b5273d56685'>
                    <saml:Issuer>http://idp.example.com/metadata.php</saml:Issuer>
                    <samlp:Status>
                        <samlp:StatusCode Value='urn:oasis:names:tc:SAML:2.0:status:Success'/>
                    </samlp:Status>
                </samlp:Response>";

            #endregion

            #region Act

            Response result = this.service.Deserialize<Response>(response);

            #endregion

            #region Assert

            Assert.NotNull(result);
            Assert.Equal("http://idp.example.com/metadata.php", result.Issuer.Value);
            Assert.Equal("urn:oasis:names:tc:SAML:2.0:status:Success", result.Status.StatusCode.Value);

            #endregion  
        }
    }
}