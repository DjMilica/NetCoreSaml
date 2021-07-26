using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionAuthnStatementValidator
    {
        Task Validate(AuthnStatement authnStatement);
    }

    public class AuthnResponseAssertionAuthnStatementValidator : IAuthnResponseAssertionAuthnStatementValidator
    {

        public AuthnResponseAssertionAuthnStatementValidator() 
        {
        }

        public async Task Validate(AuthnStatement authnStatement)
        {
            
        }
    }
}
