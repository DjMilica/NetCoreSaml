using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAuthnResponseAssertionAttributeStatementValidator
    {
        Task Validate(AttributeStatement attributeStatement);
    }

    public class AuthnResponseAssertionAttributeStatementValidator : IAuthnResponseAssertionAttributeStatementValidator
    {

        public AuthnResponseAssertionAttributeStatementValidator() 
        {
        }

        public async Task Validate(AttributeStatement attributeStatement)
        {
            
        }
    }
}
