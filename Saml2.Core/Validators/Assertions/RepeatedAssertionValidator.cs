using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using Saml2.Core.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Saml2.Core.Validators.Assertions
{
    public interface IRepeatedAssertionValidator
    {
        Task Validate(Assertion assertion);
    }

    public class RepeatedAssertionValidator : IRepeatedAssertionValidator
    {
        private readonly AuthnResponseContext authnResponseContext;
        private readonly IAssertionStore assertionStore;

        public RepeatedAssertionValidator(
            AuthnResponseContext authnResponseContext,
            IAssertionStore assertionStore
        ) 
        {
            this.authnResponseContext = authnResponseContext;
            this.assertionStore = assertionStore;
        }

        public async Task Validate(Assertion assertion)
        {
            if (this.authnResponseContext.BearerSubjectConfirmationsData.Count == 0)
            {
                return;
            }

            bool assertionIsRepeated = await this.assertionStore.CheckIfExists(assertion.Id);

            SamlValidationGuard.NotTrue(
                !assertionIsRepeated,
                $"Received assertion is repeated. This SP already received assertion with id {assertion.Id}."
            );

            DateTime? minimalNotOnOrAfter = null;

            foreach(SubjectConfirmationData subjectConfirmationData in this.authnResponseContext.BearerSubjectConfirmationsData)
            {
                if (!minimalNotOnOrAfter.HasValue || minimalNotOnOrAfter.Value > subjectConfirmationData.NotOnOrAfter)
                {
                    minimalNotOnOrAfter = subjectConfirmationData.NotOnOrAfter;
                } 
            }

            await this.assertionStore.Insert(assertion.Id, minimalNotOnOrAfter.Value);
        }
    }
}
