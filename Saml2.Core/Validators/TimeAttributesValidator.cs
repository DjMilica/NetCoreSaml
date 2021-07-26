using Saml2.Core.Errors;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Validators
{
    public interface ITimeAttributesValidator
    {
        void ValidateIssueInstant(DateTime issueInstant);
    }

    public class TimeAttributesValidator: ITimeAttributesValidator
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;

        public TimeAttributesValidator(
            ISpConfigurationProvider spConfigurationProvider
        )
        {
            this.spConfigurationProvider = spConfigurationProvider;
        }

        public void ValidateIssueInstant(DateTime issueInstant)
        {
            this.ValidateInstantAttribute(issueInstant, SamlAttributeSelector.IssueInstant);
        }

        private void ValidateInstantAttribute(DateTime instant, string attributeName)
        {
            if (!this.spConfigurationProvider.GetValidateTimeAttributes())
            {
                return;
            }

            long instantMilliseconds = instant.Ticks;

            SamlValidationGuard.NotTrue(
                instantMilliseconds != 0,
                $"Attribute {attributeName} should define valid time."
            );

            long millisecondsWithoutSkew = instantMilliseconds - this.spConfigurationProvider.GetMillisecondsSkew();

            SamlValidationGuard.NotTrue(
                millisecondsWithoutSkew < DateTime.Now.Ticks,
                $"Attribute {attributeName} should not be in the future."
            );
        }
    }
}
