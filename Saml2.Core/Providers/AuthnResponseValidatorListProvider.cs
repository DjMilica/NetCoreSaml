using Saml2.Core.Models.Xml;
using Saml2.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Providers
{
    public interface IAuthnResponseValidatorListProvider
    {
        List<ISamlAuthnResponseValidator> Get();
    }

    public class AuthnResponseValidatorListProvider: IAuthnResponseValidatorListProvider
    {
        private readonly List<ISamlAuthnResponseValidator> validators = new List<ISamlAuthnResponseValidator>();

        public AuthnResponseValidatorListProvider(
            AuthnResponseIssuerValidator authnResponseIssuerValidator,
            AuthResponseAttributeValidator authResponseAttributeValidator,
            AuthnResponseStatusValidator authnResponseStatusValidator,
            AuthnResponseSignatureValidator authnResponseSignatureValidator,
            AuthnResponseDecryptAssertionValidator authnResponseDecryptAssertionValidator,
            AuthnResponseAssertionValidator authnResponseAssertionValidator
        )
        {
            this.validators.Add(authnResponseIssuerValidator);
            this.validators.Add(authResponseAttributeValidator);
            this.validators.Add(authnResponseStatusValidator);
            this.validators.Add(authnResponseDecryptAssertionValidator);
            this.validators.Add(authnResponseSignatureValidator);
            this.validators.Add(authnResponseAssertionValidator);
        }

        public List<ISamlAuthnResponseValidator> Get()
        {
            return this.validators;
        }

    }
}
