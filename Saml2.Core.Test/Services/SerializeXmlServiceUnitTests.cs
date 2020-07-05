using Saml2.Core.Services;
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
    }
}
