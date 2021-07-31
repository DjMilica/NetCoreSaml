using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using Saml2.Core.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IAssertionSubjectConfirmationDataValidator
    {
        Task Validate(SubjectConfirmationData subjectConfirmationData, string method);
    }

    public class AssertionSubjectConfirmationDataValidator : IAssertionSubjectConfirmationDataValidator
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;
        private readonly ITimeAttributesValidator timeAttributesValidator;
        private readonly IAuthnRequestStore authnRequestStore;
        private readonly AuthnResponseContext authnResponseContext;

        public AssertionSubjectConfirmationDataValidator(
            ISpConfigurationProvider spConfigurationProvider,
            ITimeAttributesValidator timeAttributesValidator,
            IAuthnRequestStore authnRequestStore,
            AuthnResponseContext authnResponseContext
        )
        {
            this.spConfigurationProvider = spConfigurationProvider;
            this.timeAttributesValidator = timeAttributesValidator;
            this.authnRequestStore = authnRequestStore;
            this.authnResponseContext = authnResponseContext;
        }

        public async Task Validate(SubjectConfirmationData subjectConfirmationData, string method)
        {
            // https://docs.oasis-open.org/security/saml/v2.0/saml-profiles-2.0-os.pdf
            // The bearer <SubjectConfirmation> element described above MUST contain a <SubjectConfirmationData> element.
            if (method == SamlConstant.BearerConfirmationMethod)
            {
                SamlValidationGuard.NotNull(
                    subjectConfirmationData,
                    "Subject confirmation data should be defined in bearer confirmation method."
                );

                SamlValidationGuard.NotTrue(
                    subjectConfirmationData.NotOnOrAfter != null && subjectConfirmationData.NotOnOrAfter.Ticks != 0,
                    "Bearer SubjectConfirmation must contain NotOnOrAfter attribute that limits the window during which the assertion can be delivered."
                );

                SamlValidationGuard.NotTrue(
                    subjectConfirmationData.NotBefore == null || subjectConfirmationData.NotBefore.Ticks == 0,
                    "Bearer SubjectConfirmation must NOT contain NotBefore attribute."
                );

                SamlValidationGuard.NotNullOrEmptyString(
                    subjectConfirmationData.Recipient,
                    "Bearer SubjectConfirmation must contain Recipient attribute."
                );

                string authnResponseLocation = this.spConfigurationProvider.GetAuthenticationResponseLocation();

                SamlValidationGuard.NotTrue(
                    subjectConfirmationData.Recipient == authnResponseLocation,
                    $"Bearer Recipient attribute should have authentication response location {authnResponseLocation}. Received value  is {subjectConfirmationData.Recipient}."
                );

                this.authnResponseContext.bearerSubjectConfirmationsData.Add(subjectConfirmationData);
            }
            else if (subjectConfirmationData == null)
            {
                return;
            }

            this.timeAttributesValidator.ValidateNotBeforeAndNotOnOrAfter(subjectConfirmationData.NotBefore, subjectConfirmationData.NotOnOrAfter);
        
            if (subjectConfirmationData.InResponseTo.IsNotNullOrWhitspace())
            {
                bool requestExists = await this.authnRequestStore.CheckIfExists(subjectConfirmationData.InResponseTo);

                SamlValidationGuard.NotTrue(
                    requestExists,
                    "Subject confirmation data received invalid InResponseTo attribute!"
                );
            }
        }
    }
}
