using Microsoft.Extensions.Logging;
using Saml2.Core.Constants;
using Saml2.Core.Enums;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System;

namespace Saml2.Core.Factories
{
    public interface IAuthnRequestFactory
    {
        AuthnRequest Create();
    }

    public class AuthnRequestFactory: IAuthnRequestFactory
    {
        private readonly ILogger logger;
        private readonly IIdpConfigurationProviderFactory idpConfigurationProviderFactory;
        private readonly ISpConfigurationProvider spConfigurationProvider;

        public AuthnRequestFactory(
            ILogger<AuthnRequestFactory> logger,
            IIdpConfigurationProviderFactory idpConfigurationProviderFactory,
            ISpConfigurationProvider spConfigurationProvider
        )
        {
            this.logger = logger;
            this.idpConfigurationProviderFactory = idpConfigurationProviderFactory;
            this.spConfigurationProvider = spConfigurationProvider;
        }

        public AuthnRequest Create()
        {
            IIdpConfigurationProvider idpConfigurationProvider =
                this.idpConfigurationProviderFactory.Get();

            AuthnRequest request = new AuthnRequest()
            {
                Id = $"_{ Guid.NewGuid() }",
                IssueInstant = DateTime.UtcNow,
                Version = SamlConstant.Version,
                Destination = idpConfigurationProvider.GetRedirectBindingAuthnEndpoint(),
                Issuer = new Issuer() { Value = this.spConfigurationProvider.GetEntityId() },
                ProtocolBinding = BindingType.HttpPost.ToDescriptionString(),
                AssertionConsumerServiceUrl = this.spConfigurationProvider.GetAuthenticationResponseLocation(),
                ForceAuthn = true
            };


            return request;
        }
    }
}
