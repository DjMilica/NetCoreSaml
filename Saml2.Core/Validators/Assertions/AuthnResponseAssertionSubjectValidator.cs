using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionSubjectValidator
    {
        Task Validate(Subject subject);
    }

    public class AuthnResponseAssertionSubjectValidator : IAuthnResponseAssertionSubjectValidator
    {
        private readonly INameIdFormatValidator nameIdFormatValidator;
        private readonly IAssertionSubjectConfirmationValidator assertionSubjectConfirmationValidator;

        public AuthnResponseAssertionSubjectValidator(
            INameIdFormatValidator nameIdFormatValidator,
            IAssertionSubjectConfirmationValidator assertionSubjectConfirmationValidator
        ) 
        {
            this.nameIdFormatValidator = nameIdFormatValidator;
            this.assertionSubjectConfirmationValidator = assertionSubjectConfirmationValidator;
        }

        public async Task Validate(Subject subject)
        {
            if (subject == null)
            {
                return;
            }

            if (subject.NameId == null && subject.EncryptedId == null && !(subject.SubjectConfirmations?.Count > 0))
            {
                throw new SamlValidationException("Minimum one of the <SubjectConfirmation> and <NameId> should be defined in <Subject>.");
            }

            if (subject.EncryptedId != null)
            {
               // TODO should decrypt nameId first
            }

            this.nameIdFormatValidator.Validate(subject.NameId);

            await this.assertionSubjectConfirmationValidator.ValidateList(subject.SubjectConfirmations);
        }
    }
}
