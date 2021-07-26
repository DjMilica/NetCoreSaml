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
    public interface IAuthnResponseAssertionIssuerValidator
    {
        Task Validate(Issuer issuer);
    }

    public class AuthnResponseAssertionIssuerValidator: IAuthnResponseAssertionIssuerValidator
    {
        private readonly INameIdFormatValidator nameIdFormatValidator;
        private readonly AuthnResponseContext authnResponseContext;

        public AuthnResponseAssertionIssuerValidator(
            INameIdFormatValidator nameIdFormatValidator,
            AuthnResponseContext authnResponseContext
        )
        {
            this.nameIdFormatValidator = nameIdFormatValidator;
            this.authnResponseContext = authnResponseContext;
        }

        public async Task Validate(Issuer issuer)
        {
            SamlValidationGuard.NotNull(
                issuer,
                "Assertion should have issuer defined!"
            );

            SamlValidationGuard.NotNullOrEmptyString(
                issuer.Value,
                "Issuer entity id could not be null in saml assertion"
            );

            if (issuer.Format.IsNotNullOrWhitspace() && issuer.Format != NameIdFormat.ENTITY)
            {
                throw new SamlValidationException(
                    "Assertion does not have entity format in issuer element. Sent format is {0}.",
                    issuer.Format
                );
            }

            SamlValidationGuard.NotTrue(
                this.authnResponseContext.idpConfigurationProvider.GetEntityId() == issuer.Value,
                $"Assertion has unknown issuer {issuer.Value}."
            );

            this.nameIdFormatValidator.ValidateEntityFormat(issuer.Value, issuer.SpNameQualifier);
        }
    }
}
