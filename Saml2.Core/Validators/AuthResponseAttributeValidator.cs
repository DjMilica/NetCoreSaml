using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extenstions;
using Saml2.Core.Models.Xml;
using System;

namespace Saml2.Core.Validators
{
    public class AuthResponseAttributeValidator : ISamlAuthnResponseElementValidator<AuthnResponse>
    {
        public void Validate(AuthnResponse data, AuthnResponseContext context)
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

            if (data.IssueInstant.Millisecond > DateTime.UtcNow.Millisecond)
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
