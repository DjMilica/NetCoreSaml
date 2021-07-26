using Saml2.Core.Errors;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public interface IAudienceRestrictionValidator
    {
        Task ValidateOptionalList(List<AudienceRestriction> audienceRestrictions);
        Task Validate(AudienceRestriction audienceRestriction);
    }

    public class AudienceRestrictionValidator : IAudienceRestrictionValidator
    {
        private readonly ISpConfigurationProvider spConfigurationProvider;

        public AudienceRestrictionValidator(
            ISpConfigurationProvider spConfigurationProvider
        )
        {
            this.spConfigurationProvider = spConfigurationProvider;
        }

        public async Task Validate(AudienceRestriction audienceRestriction)
        {
            if (audienceRestriction.Audiences == null || audienceRestriction.Audiences.Count == 0)
            {
                throw new SamlValidationException("AudienceRestriction element should have at least one Audience subelement.");
            }

            string spEntityId = this.spConfigurationProvider.GetEntityId();

            Audience spAudience = audienceRestriction.Audiences.Find(x => x.Value == spEntityId);

            SamlValidationGuard.NotNull(
                spAudience,
                $"The Service Provider with entityId {spEntityId} is not a valid audience for the assertion."
            );
        }

        public async Task ValidateOptionalList(List<AudienceRestriction> audienceRestrictions)
        {
            if (audienceRestrictions != null && audienceRestrictions.Count > 0)
            {
                foreach (AudienceRestriction audienceRestriction in audienceRestrictions)
                {
                    await this.Validate(audienceRestriction);
                }
            }
        }
    }
}
