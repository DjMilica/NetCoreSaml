using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionConditionsValidator
    {
        Task Validate(Conditions conditions);
    }

    public class AuthnResponseAssertionConditionsValidator : IAuthnResponseAssertionConditionsValidator
    {

        public AuthnResponseAssertionConditionsValidator() 
        {
        }

        public async Task Validate(Conditions conditions)
        {
            
        }
    }
}
