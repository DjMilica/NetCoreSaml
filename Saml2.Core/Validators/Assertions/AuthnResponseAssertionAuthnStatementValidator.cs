using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionAuthnStatementValidator
    {
        Task ValidateOptionalList(List<AuthnStatement> authnStatements);
        Task Validate(AuthnStatement authnStatement);
    }

    public class AuthnResponseAssertionAuthnStatementValidator : IAuthnResponseAssertionAuthnStatementValidator
    {
        private readonly ITimeAttributesValidator timeAttributesValidator;
        private readonly IAuthnContextValidator authnContextValidator;

        public AuthnResponseAssertionAuthnStatementValidator(
            ITimeAttributesValidator timeAttributesValidator,
            IAuthnContextValidator authnContextValidator
        ) 
        {
            this.timeAttributesValidator = timeAttributesValidator;
            this.authnContextValidator = authnContextValidator;
        }

        public async Task Validate(AuthnStatement authnStatement)
        {
            this.timeAttributesValidator.ValidateAuthnInstant(authnStatement.AuthnInstant);

            if (authnStatement.SessionIndex != null && authnStatement.SessionIndex.HasOnlyWhitespaceChars())
            {
                throw new SamlValidationException("If session index is sent in authn statement, it should have some chars different than whitespace.");
            }

            if (authnStatement.SubjectLocality != null)
            {
                this.ValidateSubjectLocality(authnStatement.SubjectLocality);
            }

            await this.authnContextValidator.Validate(authnStatement.AuthnContext);    
        }

        public async Task ValidateOptionalList(List<AuthnStatement> authnStatements)
        {
            if (authnStatements != null)
            {
                foreach (AuthnStatement authnStatement in authnStatements)
                {
                    await this.Validate(authnStatement);
                }
            }
        }

        private void ValidateSubjectLocality(SubjectLocality subjectLocality)
        {
            if (subjectLocality.Address != null && subjectLocality.Address.HasOnlyWhitespaceChars())
            {
                throw new SamlValidationException("If subject locality address is sent in authn statement, it should have some chars different than whitespace.");
            }

            if (subjectLocality.DnsName != null && subjectLocality.DnsName.HasOnlyWhitespaceChars())
            {
                throw new SamlValidationException("If subject locality dns name is sent in authn statement, it should have some chars different than whitespace.");
            }
        }
    }
}
