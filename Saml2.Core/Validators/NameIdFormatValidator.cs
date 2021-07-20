﻿using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Validators
{
    public interface INameIdFormatValidator
    {
        void Validate(BaseNameId nameIdElement);
        void ValidateEntityFormat(string value, string spNameQualifier);
    }

    public class NameIdFormatValidator : INameIdFormatValidator
    {
        public void Validate(BaseNameId nameIdElement)
        {
            if (!nameIdElement.Format.IsNotNullOrWhitspace())
            {
                return;
            }

            SamlValidationGuard.NotNullOrEmptyString(
                nameIdElement.Value, 
                "Tag with ${ nameId.format} Format attribute MUST contain a Value that contains more than whitespace characters"
            );

            switch (nameIdElement.Value)
            {
                case NameIdFormat.ENTITY:
                    this.ValidateEntityFormat(nameIdElement.Value, nameIdElement.SpNameQualifier);
                    break;
                case NameIdFormat.UNSPECIFIED:
                case NameIdFormat.WINDOWS:
                case NameIdFormat.X509_SUBJECT_NAME:
                    break;
                default:
                    throw new SamlValidationException("Invalid NameID format.");
            }
        }

        public void ValidateEntityFormat(string value, string spNameQualifier)
        {
            if (value.Length > 1024)
            {
                throw new SamlValidationException("Tag with Entity Format attribute MUST have a Value that contains no more than 1024 characters");
            }

            if (spNameQualifier.IsNotNullOrWhitspace())
            {
                throw new SamlValidationException("Tag with Entity Format attribute MUST NOT set the SPNameQualifier attribute");
            }
        }
    }
}
