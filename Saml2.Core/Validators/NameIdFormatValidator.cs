using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            switch (nameIdElement.Format)
            {
                case NameIdFormat.ENTITY:
                    this.ValidateEntityFormat(nameIdElement.Value, nameIdElement.SpNameQualifier);
                    break;
                case NameIdFormat.EMAIL:
                    this.ValidateEmailFormat(nameIdElement.Value);
                    break;
                case NameIdFormat.PERSISTENT:
                    this.ValidatePersistentFormat(nameIdElement.Value);
                    break;
                case NameIdFormat.TRANSIENT:
                    this.ValidateTransientFormat(nameIdElement.Value);
                    break;
                case NameIdFormat.KERBEROS:
                    this.ValidateKerberosFormat(nameIdElement.Value);
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

        public void ValidateEmailFormat(string value)
        {
            bool isValid = value != null && new EmailAddressAttribute().IsValid(value);

            if (!isValid)
            {
                throw new SamlValidationException("Tag value with Email Format attribute is not a valid email address according to the IETF RFC 2822 specification");
            }
        }

        public void ValidatePersistentFormat(string value)
        {
            if (value.Length > 256)
            {
                throw new SamlValidationException("Tag with Persistent Format attribute MUST have a Value that contains no more than 256 characters");
            }
        }

        public void ValidateTransientFormat(string value)
        {
            if (value.Length > 256)
            {
                throw new SamlValidationException("Tag with Transient Format attribute MUST have a Value that contains no more than 256 characters");
            }
        }

        public void ValidateKerberosFormat(string value)
        {
            if (value.Length < 3)
            {
                throw new SamlValidationException("Tag with Kerberos Format attribute MUST contain a Value with at least 3 characters");
            }

            if (value.IndexOf('@') < 0)
            {
                throw new SamlValidationException("Tag with Kerberos Format attribute MUST contain a Value that contains a '@'");
            }
        }
    }
}
