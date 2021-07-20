using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthnResponseDecryptAssertionValidator : BaseAuthnResponseValidator
    {
        public AuthnResponseDecryptAssertionValidator(
            AuthnResponseContext authnResponseContext
        ) : base(authnResponseContext)
        { }

        public override async Task Validate(AuthnResponse data)
        {

        }
    }
}
