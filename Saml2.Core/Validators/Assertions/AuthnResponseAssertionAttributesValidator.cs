using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionAttributesValidator
    {
        Task Validate(Assertion assertion);
    }

    public class AuthnResponseAssertionAttributesValidator : IAuthnResponseAssertionAttributesValidator
    {
        private readonly ITimeAttributesValidator timeAttributesValidator;

        public AuthnResponseAssertionAttributesValidator(
            ITimeAttributesValidator timeAttributesValidator
        ) 
        {
            this.timeAttributesValidator = timeAttributesValidator;
        }

        public async Task Validate(Assertion assertion)
        {
            SamlValidationGuard.NotTrue(
                assertion.Id.IsValidSamlId(),
                SamlValidationErrors.IdAttributeShouldNotBeNullOrUndefined,
                SamlElementSelector.Assertion
            );

            SamlValidationGuard.NotTrue(
                assertion.Version == SamlConstant.Version,
                $"Assertion has version {assertion.Version} instead of {SamlConstant.Version}!"
            );

            this.timeAttributesValidator.ValidateIssueInstant(assertion.IssueInstant);
        }
    }
}
