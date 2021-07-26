using Saml2.Core.Errors;
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
            if (attributeStatement.EncryptedAttributes != null)
            {
                // TODO decrypt these encrypted attributes
            } 

            if (attributeStatement.Attributes == null || attributeStatement.Attributes.Count == 0)
            {
                throw new SamlValidationException("AttributeStatement element should have at least one Attribute subelement");
            }


            foreach(Models.Xml.Attribute attribute in attributeStatement.Attributes)
            {
                SamlValidationGuard.NotNullOrEmptyString(
                    attribute.Name,
                    "Name attribute inside Attribute element should not be null or empty"
                );

                if (attribute.AttributeValues == null || attribute.AttributeValues.Length == 0)
                {
                    continue;
                }

                string firstAttributeType = attribute.AttributeValues[0].Type;

                foreach(BaseAttributeValue attributeValue in attribute.AttributeValues)
                {
                    if (attributeValue.Type != firstAttributeType)
                    {
                        throw new SamlValidationException("All attribute values in one attribute element must have same data type defined!");
                    }

                    // If a SAML attribute includes a "null" value, the corresponding <AttributeValue> element MUST be
                    // empty and MUST contain the reserved xsi:nil XML attribute with a value of "true" or "1"
                    bool valueShouldBeNull = attributeValue.Nil != null && (attributeValue.Nil.Trim() == "true" || attributeValue.Nil.Trim() == "1");

                    if (valueShouldBeNull && attributeValue.GetValue() != null)
                    {
                        throw new SamlValidationException("Null attribute values MUST be empty and and MUST contain the reserved xsi:nil XML attribute.");
                    }
                }

            }
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
