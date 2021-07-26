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
        void ValidateAuthnInstant(DateTime authnInstant);
        void ValidateNotBeforeAndNotOnOrAfter(DateTime notBefore, DateTime notOnOrAfter);
        void ValidateNotBefore(DateTime notBefore);
        void ValidateNotOnOrAfter(DateTime notOnOrAfter);
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

        public void ValidateAuthnInstant(DateTime authnInstant)
        {
            this.ValidateInstantAttribute(authnInstant, SamlAttributeSelector.AuthnInstant);
        }

        public void ValidateNotBeforeAndNotOnOrAfter(DateTime notBefore, DateTime notOnOrAfter)
        {
            bool notBeforeHasValue = notBefore != null && notBefore.Ticks != 0;
            bool notOnOrAfterHasValue = notOnOrAfter != null && notOnOrAfter.Ticks != 0;

            if ((notBefore == null || notBefore.Ticks == 0) && (notOnOrAfter == null || notOnOrAfter.Ticks == 0)) {
                return;
            }

            if (notBeforeHasValue)
            {
                this.ValidateNotBefore(notBefore);
            }

            if (notOnOrAfterHasValue)
            {
                this.ValidateNotOnOrAfter(notOnOrAfter);
            }

            if (notBeforeHasValue && notOnOrAfterHasValue && notBefore.Ticks >= notOnOrAfter.Ticks)
            {
                throw new SamlValidationException($"Attribute NotBefore {notBefore} MUST be less than NotOnOrAfter {notOnOrAfter}");
            }
        }

        public void ValidateNotBefore(DateTime notBefore)
        {
            if (!this.spConfigurationProvider.GetValidateTimeAttributes())
            {
                return;
            }

            long notBeforeWithoutSkew = notBefore.Ticks - this.spConfigurationProvider.GetMillisecondsSkew();

            SamlValidationGuard.NotTrue(
                notBeforeWithoutSkew < DateTime.UtcNow.Ticks,
                $"Attribute NotBefore {notBefore} should not be in the future."
            );

        }

        public void ValidateNotOnOrAfter(DateTime notOnOrAfter)
        {
            if (!this.spConfigurationProvider.GetValidateTimeAttributes())
            {
                return;
            }

            long notOnOrAfterWithSkew = notOnOrAfter.Ticks + this.spConfigurationProvider.GetMillisecondsSkew();

            SamlValidationGuard.NotTrue(
                notOnOrAfterWithSkew > DateTime.UtcNow.Ticks,
                $"Attribute NotOnOrAfter {notOnOrAfter} should not be in the past."
            );
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
                millisecondsWithoutSkew < DateTime.UtcNow.Ticks,
                $"Attribute {attributeName} should not be in the future."
            );
        }
    }
}
