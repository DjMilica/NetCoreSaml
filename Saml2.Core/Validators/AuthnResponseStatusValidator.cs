using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public class AuthnResponseStatusValidator : BaseAuthnResponseValidator
    {
        public AuthnResponseStatusValidator(
            AuthnResponseContext authnResponseContext
        ) : base(authnResponseContext)
        {}

        public override async Task Validate(AuthnResponse data)
        {
            SamlValidationGuard.NotNull(
                data.Status,
                "Status element should not be null inside authn response!"
            );

            SamlValidationGuard.NotNullOrEmptyString(
                data.Status.StatusCode?.Value,
                "Status element should have status code inside authn response!"
            );

            if (data.Status.StatusCode.Value != StatusCodesConstant.SUCCESS.Name)
            {
                string subCodesMessage = this.SecondLevelStatusCodesToString(data.Status.StatusCode);
                string statusMessagesMessage = data.Status.StatusMessage.IsNotNullOrWhitespace() ? $"Status message is: [${data.Status.StatusMessage}]." : string.Empty;
                string topLevelStatusCodeMessage = $"Top level status code is { this.GetStatusCodeInformation(data.Status.StatusCode.Value)}.";

                throw new SamlValidationException(
                    "Unsuccessful SAML response! {0} {1} {2}",
                    topLevelStatusCodeMessage,
                    subCodesMessage,
                    statusMessagesMessage
                );
            }
        }

        private string SecondLevelStatusCodesToString(StatusCode statusCode) {
            if (statusCode.SubCodes == null)
            {
                return string.Empty;
            }

            if (statusCode.SubCodes.Count == 0)
            {
                return string.Empty;
            }

            string subcodeString = $"Second level status codes are: ";

            foreach (StatusCode code in statusCode.SubCodes) {
                subcodeString = $"{subcodeString}, {this.GetStatusCodeInformation(code.Value)}";
            }

            return subcodeString;
        }

        private string GetStatusCodeInformation(string value) {
            string additionalInformation = StatusCodesConstant.GetDetails(value) ?? "No additional information.";

            return $"{value}:${additionalInformation}";
        }
    }
}
