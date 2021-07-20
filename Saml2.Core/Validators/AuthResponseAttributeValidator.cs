using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using Saml2.Core.Stores;
using System;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthResponseAttributeValidator : BaseAuthnResponseValidator
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly IAuthnRequestStore authnRequestStore;

        public AuthResponseAttributeValidator(
            AuthnResponseContext authnResponseContext,
            ISpConfigurationProvider spConfigurationProvider,
            IAuthnRequestStore authnRequestStore
        ) : base(authnResponseContext)
        {
            this.spConfigurationProvider = spConfigurationProvider;
            this.authnRequestStore = authnRequestStore;
        }

        public override async Task Validate(AuthnResponse data)
        {
            SamlValidationGuard.NotTrue(
                data.Id.IsValidSamlId(),
                SamlValidationErrors.IdAttributeShouldNotBeNullOrUndefined,
                SamlElementSelector.AuthnResponse
            );

            SamlValidationGuard.NotNull(
                data.IssueInstant,
                SamlValidationErrors.TimeAttributeShouldNotBeNull,
                SamlAttributeSelector.IssueInstant,
                SamlElementSelector.AuthnResponse
            );

            if (data.IssueInstant.Ticks > DateTime.UtcNow.Ticks)
            {
                throw new SamlValidationException(
                    SamlValidationErrors.IssueInstantShouldNotBeInFuture,
                    SamlElementSelector.AuthnResponse
                );
            }

            if (data.Version != SamlConstant.Version)
            {
                throw new SamlValidationException(
                    SamlValidationErrors.VersionMissmatch,
                    data.Version,
                    SamlConstant.Version
                );
            }

            SamlValidationGuard.NotNullOrEmptyString(
                data.Destination,
                "Destination attribute in AuthnResponse should not be empty"
            );

            string expectedResponseHandlerDestination = this.spConfigurationProvider.GetAuthenticationResponseLocation();

            if (data.Destination != expectedResponseHandlerDestination)
            {
                throw new SamlValidationException(
                    "Received authn response destination {0} does not match expected destination {1}.",
                    data.Destination,
                    expectedResponseHandlerDestination
                );
            }

            if (data.InResponseTo.IsNotNullOrWhitspace())
            {
                bool requestExists = await this.authnRequestStore.CheckIfExists(data.InResponseTo);

                SamlValidationGuard.NotTrue(
                    requestExists,
                    "Authn Response received invalid InResponseTo attribute!"
                );
            }
        }
    }
}
