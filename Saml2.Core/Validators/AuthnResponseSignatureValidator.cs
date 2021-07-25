using Saml2.Core.Errors;
using Saml2.Core.Helpers;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthnResponseSignatureValidator : BaseAuthnResponseValidator
    {
        private readonly ISamlSignatureHelper samlSignatureHelper;

        public AuthnResponseSignatureValidator(
            AuthnResponseContext authnResponseContext,
            ISamlSignatureHelper samlSignatureHelper
        ) : base(authnResponseContext)
        {
            this.samlSignatureHelper = samlSignatureHelper;
        }

        public override async Task Validate(AuthnResponse data)
        {
            if (data.Signature == null)
            {
                foreach(Assertion assertion in data.Assertions)
                {
                    if (assertion.Signature == null)
                    {
                        throw new SamlValidationException("If whole authn response is not signed, all assertions should be signed.");
                    }
                }
            }

            bool isSignatureValid = this.samlSignatureHelper.VerifyXmlSignature(this.authnResponseContext.StringifiedResponse);

            SamlValidationGuard.NotTrue(
                isSignatureValid,
                "Authn response Signature is not valid!"
            );

        }
    }
}
