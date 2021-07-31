using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using Saml2.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Validators
{
    public interface INameIdValidator
    {
        void ValidateInSubjectConfirmation(SubjectConfirmation subjectConfirmation);
        void ValidateInSubject(Subject subject);

    }

    public class NameIdValidator: INameIdValidator
    {
        private readonly INameIdFormatValidator nameIdFormatValidator;
        private readonly IDecryptXmlElementService decryptXmlElementService;
        private readonly AuthnResponseContext authnResponseContext;

        public NameIdValidator(
            INameIdFormatValidator nameIdFormatValidator,
            IDecryptXmlElementService decryptXmlElementService,
            AuthnResponseContext authnResponseContext
        ) 
        {
            this.nameIdFormatValidator = nameIdFormatValidator;
            this.decryptXmlElementService = decryptXmlElementService;
            this.authnResponseContext = authnResponseContext;
        }

        public void ValidateInSubjectConfirmation(SubjectConfirmation subjectConfirmation)
        {
            NameId nameId = this.ValidateOptional(subjectConfirmation.NameId, subjectConfirmation.EncryptedId);

            subjectConfirmation.NameId = nameId;
        }

        public void ValidateInSubject(Subject subject)
        {
            NameId nameId = this.ValidateOptional(subject.NameId, subject.EncryptedId);

            subject.NameId = nameId;
        }

        private NameId ValidateOptional(NameId nameId, EncryptedID encryptedId)
        {
            if (nameId == null && encryptedId != null)
            {
                nameId = this.decryptXmlElementService.DecryptElementFromParsedXml<NameId, EncryptedID>(
                    encryptedId,
                    SamlElementSelector.EncryptedId
                );
            }

            if (nameId != null)
            {
                this.nameIdFormatValidator.Validate(nameId);
            }

            return nameId;
        }
    }
}
