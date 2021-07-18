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
            AuthResponseAttributeValidator authResponseAttributeValidator
        )
        {
            this.validators.Add(authResponseAttributeValidator);
        }

        public List<ISamlAuthnResponseValidator> Get()
        {
            return this.validators;
        }

    }
}
