using Saml2.Core.Models.Xml;
using Saml2.Core.XsdSchema;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthnResponseXsdSchemaValidator : BaseAuthnResponseValidator
    {
        private readonly IXmlSchemaValidator xsdSchemaValidator;

        public AuthnResponseXsdSchemaValidator(
            AuthnResponseContext authnResponseContext,
            IXmlSchemaValidator xsdSchemaValidator
        ) : base(authnResponseContext)
        {
            this.xsdSchemaValidator = xsdSchemaValidator;
        }

        public override async Task Validate(AuthnResponse data)
        {
            this.xsdSchemaValidator.ValidateAuthnResponse(this.authnResponseContext.StringifiedResponse);
        }
    }
}
