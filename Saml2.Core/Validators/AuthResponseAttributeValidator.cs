using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthResponseAttributeValidator : ISamlAuthnResponseValidator
    {
        private readonly AuthnResponseContext authnResponseContext;

        public AuthResponseAttributeValidator(
            AuthnResponseContext authnResponseContext
        )
        {
            this.authnResponseContext = authnResponseContext;
        }

        public async Task Validate(AuthnResponse data)
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

            // TODO Destination
            // TODO InResponseTo
        }
    }
}
