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
        Task ValidateOptionalList(List<AttributeStatement> attributeStatements);
    }

    public class AuthnResponseAssertionAttributeStatementValidator : IAuthnResponseAssertionAttributeStatementValidator
    {

        public AuthnResponseAssertionAttributeStatementValidator() 
        {
        }

        public async Task Validate(AttributeStatement attributeStatement)
        {
            
        }

        public async Task ValidateOptionalList(List<AttributeStatement> attributeStatements)
        {
            if (attributeStatements != null)
            {
                foreach (AttributeStatement attributeStatement in attributeStatements)
                {
                    await this.Validate(attributeStatement);
                }
            }
        }
    }
}
