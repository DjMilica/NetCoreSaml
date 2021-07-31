using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Helpers;
using Saml2.Core.Models.Xml;
using Saml2.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public class AuthnResponseAssertionListValidator : BaseAuthnResponseValidator
    {
        private readonly IAuthnResponseAssertionValidator authnResponseAssertionValidator;
        private readonly IDecryptXmlElementService decryptXmlElementService;

        public AuthnResponseAssertionListValidator(
            AuthnResponseContext authnResponseContext,
            IAuthnResponseAssertionValidator authnResponseAssertionValidator,
            IDecryptXmlElementService decryptXmlElementService
        ) : base(authnResponseContext)
        {
            this.authnResponseAssertionValidator = authnResponseAssertionValidator;
            this.decryptXmlElementService = decryptXmlElementService;
        }

        public override async Task Validate(AuthnResponse data)
        {
            if (data.EncryptedAssertions != null && data.EncryptedAssertions.Count > 0)
            {
                List<Assertion> decryptedAssertions = this.decryptXmlElementService.DecryptElementsFromXml<Assertion>(
                    this.authnResponseContext.StringifiedResponse,
                    SamlElementSelector.EncryptedAssertion
                );

                if (data.Assertions != null)
                {
                    data.Assertions.AddRange(decryptedAssertions);
                } 
                else
                {
                    data.Assertions = decryptedAssertions;
                }
            }

            // There should be at least one assertion with authn statement
            List<Assertion> assertionsWithAuthnStatements = data.Assertions.FindAll(x => x.AuthnStatements != null && x.AuthnStatements.Count > 0);


            if (assertionsWithAuthnStatements == null || assertionsWithAuthnStatements.Count == 0)
            {
                throw new SamlValidationException("Authn response should have at least one assertion with authn statement!");
            }

            // There should be at least one assertion with authn statement that has bearer conf method
            Assertion bearerAssertion = assertionsWithAuthnStatements.Find(x =>
            {
                SubjectConfirmation bearerConf = x.Subject?.SubjectConfirmations?.Find(y => y.Method == SamlConstant.BearerConfirmationMethod);

                return bearerConf != null;
            });

            SamlValidationGuard.NotNull(
                bearerAssertion,
                "At least one assertion containing an <AuthnStatement> MUST contain a <Subject> element with at least one <SubjectConfirmation> element containing a Method of urn:oasis:names:tc:SAML:2.0:cm:bearer."
            );

            foreach(Assertion assertion in data.Assertions)
            {
                await this.authnResponseAssertionValidator.Validate(assertion);
            }
        }
    }
}
