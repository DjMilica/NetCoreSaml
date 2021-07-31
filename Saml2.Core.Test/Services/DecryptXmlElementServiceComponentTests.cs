using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Saml2.Core.Factories;
using Saml2.Core.Helpers;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using Saml2.Core.Services;
using Xunit;

namespace Saml2.Core.Test.Helpers
{
    public class DecryptXmlElementServiceComponentTests
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly ISamlAsymmetricKeyProvider asymmetricKeyProvider;
        private readonly ISerializeXmlService serializeXmlService;
        private readonly ILogger<ISerializeXmlService> logger;
        private readonly ISamlEncryptionHelper helper;

        private readonly IDecryptXmlElementService service;

        public DecryptXmlElementServiceComponentTests()
        {
            this.spConfigurationProvider = Substitute.For<ISpConfigurationProvider>();
            this.logger = Substitute.For<ILogger<ISerializeXmlService>>();

            this.asymmetricKeyProvider = new SamlAsymmetricKeyProvider();
            this.serializeXmlService = new SerializeXmlService(this.logger);

            this.helper = new SamlEncryptionHelper(this.serializeXmlService);

            this.service = new DecryptXmlElementService(
                spConfigurationProvider,
                asymmetricKeyProvider,
                serializeXmlService,
                helper
            );
        }

        [Fact]
        public async Task ShouldDecryptAssertion()
        {
            #region Arrange
            string response = FileHelper.Read("./TestData/SamlResponseWithEncryptedAssertion.xml");

            string key = FileHelper.Read("./TestData/privateKey.pem");

            spConfigurationProvider.GetPrivateKey().Returns(key);

            #endregion

            #region Act

            List<Assertion> result = this.service.DecryptElementsFromXml<Assertion>(response, SamlElementSelector.EncryptedAssertion);

            #endregion

            #region Assert

            Assert.NotEmpty(result);

            #endregion   
        }

        [Fact]
        public async Task ShouldDecryptAssertion_2()
        {
            #region Arrange
            string response = FileHelper.Read("./TestData/SamlResponseWithEncryptedAssertion.xml");

            string key = FileHelper.Read("./TestData/privateKey.pem");

            spConfigurationProvider.GetPrivateKey().Returns(key);

            AuthnResponse xmlResponseObject = this.serializeXmlService.Deserialize<AuthnResponse>(response);

            #endregion

            #region Act

            Assertion result = this.service.DecryptElementFromParsedXml<Assertion, EncryptedAssertion>(xmlResponseObject.EncryptedAssertions.First(), SamlElementSelector.EncryptedAssertion);

            #endregion

            #region Assert

            Assert.NotNull(result);

            #endregion   
        }
    }
}
