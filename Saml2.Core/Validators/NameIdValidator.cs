using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Validators
{
    public interface INameIdValidator
    {
        void ValidateRequired(NameId nameId, EncryptedId encryptedId);
        void ValidateOptional(NameId nameId, EncryptedId encryptedId);

    }

    public class NameIdValidator: INameIdValidator
    {
        private readonly INameIdFormatValidator nameIdFormatValidator;

        public NameIdValidator(
            INameIdFormatValidator nameIdFormatValidator
        ) 
        {
            this.nameIdFormatValidator = nameIdFormatValidator;
        }

        public void ValidateOptional(NameId nameId, EncryptedId encryptedId)
        {
            if (encryptedId != null)
            {
                // TODO should decrypt nameId first
            }

            if (nameId != null)
            {
                this.nameIdFormatValidator.Validate(nameId);
            }
        }

        public void ValidateRequired(NameId nameId, EncryptedId encryptedId)
        {
            if (encryptedId != null)
            {
                // TODO should decrypt nameId first
            }

            SamlValidationGuard.NotNull(
                nameId,
                "NameId element or encryptedId element are required."
            );

            this.nameIdFormatValidator.Validate(nameId);
        }
    }
}
