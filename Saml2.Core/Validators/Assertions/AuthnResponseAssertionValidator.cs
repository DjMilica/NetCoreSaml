using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionValidator
    {
        Task Validate(Assertion assertion);
    }

    public class AuthnResponseAssertionValidator: IAuthnResponseAssertionValidator
    {
        private readonly AuthnResponseContext authnResponseContext;
        private readonly IAuthnResponseAssertionAttributesValidator authnResponseAssertionAttributesValidator;
        private readonly IAuthnResponseAssertionIssuerValidator authnResponseAssertionIssuerValidator;
        private readonly IAuthnResponseAssertionSubjectValidator authnResponseAssertionSubjectValidator;
        private readonly IAuthnResponseAssertionConditionsValidator authnResponseAssertionConditionsValidator;
        private readonly IAuthnResponseAssertionAuthnStatementValidator authnResponseAssertionAuthnStatementValidator;
        private readonly IAuthnResponseAssertionAttributeStatementValidator authnResponseAssertionAttributeStatementValidator;
        private readonly IRepeatedAssertionValidator repeatedAssertionValidator;

        public AuthnResponseAssertionValidator(
            AuthnResponseContext authnResponseContext,
            IAuthnResponseAssertionAttributesValidator authnResponseAssertionAttributesValidator,
            IAuthnResponseAssertionIssuerValidator authnResponseAssertionIssuerValidator,
            IAuthnResponseAssertionSubjectValidator authnResponseAssertionSubjectValidator,
            IAuthnResponseAssertionConditionsValidator authnResponseAssertionConditionsValidator,
            IAuthnResponseAssertionAuthnStatementValidator authnResponseAssertionAuthnStatementValidator,
            IAuthnResponseAssertionAttributeStatementValidator authnResponseAssertionAttributeStatementValidator,
            IRepeatedAssertionValidator repeatedAssertionValidator
        ) 
        {
            this.authnResponseContext = authnResponseContext;
            this.authnResponseAssertionAttributesValidator = authnResponseAssertionAttributesValidator;
            this.authnResponseAssertionIssuerValidator = authnResponseAssertionIssuerValidator;
            this.authnResponseAssertionSubjectValidator = authnResponseAssertionSubjectValidator;
            this.authnResponseAssertionConditionsValidator = authnResponseAssertionConditionsValidator;
            this.authnResponseAssertionAuthnStatementValidator = authnResponseAssertionAuthnStatementValidator;
            this.authnResponseAssertionAttributeStatementValidator = authnResponseAssertionAttributeStatementValidator;
            this.repeatedAssertionValidator = repeatedAssertionValidator;
        }

        public async Task Validate(Assertion assertion)
        {
            await this.authnResponseAssertionAttributesValidator.Validate(assertion);

            bool subjectExists = assertion.Subject != null;
            bool attributeStatementExists = assertion.AttributeStatements?.Count > 0;
            bool authnStatementsExists = assertion.AuthnStatements?.Count > 0;

            if (!subjectExists && !attributeStatementExists && !authnStatementsExists)
            {
                throw new SamlValidationException("Assertion with no Statements must have a Subject.");
            }
            else if (!subjectExists)
            {
                throw new SamlValidationException("AuthnStatement and AttributeStatement require a Subject.");
            }

            await this.authnResponseAssertionIssuerValidator.Validate(assertion.Issuer);

            await this.authnResponseAssertionSubjectValidator.Validate(assertion.Subject);

            await this.authnResponseAssertionConditionsValidator.Validate(assertion.Conditions);

            await this.authnResponseAssertionAuthnStatementValidator.ValidateOptionalList(assertion.AuthnStatements);

            await this.authnResponseAssertionAttributeStatementValidator.ValidateOptionalList(assertion.AttributeStatements);

            await this.repeatedAssertionValidator.Validate(assertion);
        }
    }
}
