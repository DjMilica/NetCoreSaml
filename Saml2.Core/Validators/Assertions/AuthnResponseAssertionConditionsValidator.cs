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
        private readonly ITimeAttributesValidator timeAttributesValidator;
        private readonly IAudienceRestrictionValidator audienceRestrictionValidator;

        public AuthnResponseAssertionConditionsValidator(
            ITimeAttributesValidator timeAttributesValidator,
            IAudienceRestrictionValidator audienceRestrictionValidator
        ) 
        {
            this.timeAttributesValidator = timeAttributesValidator;
            this.audienceRestrictionValidator = audienceRestrictionValidator;
        }

        public async Task Validate(Conditions conditions)
        {
            if (conditions == null)
            {
                return;
            }

            this.timeAttributesValidator.ValidateNotBeforeAndNotOnOrAfter(conditions.NotBefore, conditions.NotOnOrAfter);

            await this.audienceRestrictionValidator.ValidateOptionalList(conditions.AudienceRestrictions);
        }
    }
}
