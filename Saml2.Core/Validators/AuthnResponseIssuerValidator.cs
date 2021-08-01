using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Factories;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthnResponseIssuerValidator : BaseAuthnResponseValidator
    {
        private readonly IIdpConfigurationProviderFactory idpConfigurationProviderFactory;
        private readonly INameIdFormatValidator nameIdFormatValidator;

        public AuthnResponseIssuerValidator(
            AuthnResponseContext authnResponseContext,
            IIdpConfigurationProviderFactory idpConfigurationProviderFactory,
            INameIdFormatValidator nameIdFormatValidator
        ) : base(authnResponseContext)
        {
            this.idpConfigurationProviderFactory = idpConfigurationProviderFactory;
            this.nameIdFormatValidator = nameIdFormatValidator;
        }

        public override async Task Validate(AuthnResponse data)
        {
            SamlValidationGuard.NotNull(
                data.Issuer, 
                "Issuer could not be null in saml authentication response!"
            );

            SamlValidationGuard.NotNullOrEmptyString(
                data.Issuer.Value, 
                "Issuer entity id could not be null in saml authentication response"
            );

            if (data.Issuer.Format.IsNotNullOrWhitspace() && data.Issuer.Format != NameIdFormat.ENTITY)
            {
                throw new SamlValidationException(
                    "Authentication response does not have entity format in issuer element. Sent format is {0}.", 
                    data.Issuer.Format
                );
            }

            IIdpConfigurationProvider idpConfigurationProvider = 
                    await this.idpConfigurationProviderFactory.CreateWithIdpEntityId(data.Issuer.Value);

            this.authnResponseContext.IdpConfigurationProvider = idpConfigurationProvider;

            this.nameIdFormatValidator.ValidateEntityFormat(data.Issuer.Value, data.Issuer.SpNameQualifier);
        }
    }
}
