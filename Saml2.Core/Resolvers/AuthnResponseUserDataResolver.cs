using Saml2.Core.Constants;
using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Saml2.Core.Resolvers
{
    public interface IAuthnResponseUserDataResolver
    {
        SamlUserData Resolve();
    }

    public class AuthnResponseUserDataResolver: IAuthnResponseUserDataResolver
    {
        private readonly AuthnResponseContext authnResponseContext;

        public AuthnResponseUserDataResolver(
            AuthnResponseContext authnResponseContext
        )
        {
            this.authnResponseContext = authnResponseContext;
        }

        private IIdpConfigurationProvider idpConfigurationProvider => this.authnResponseContext.IdpConfigurationProvider;

        public SamlUserData Resolve()
        {
            SamlUserData samlUserData = new SamlUserData();

            bool useNameIdAsSpUserId = this.idpConfigurationProvider.GetUseNameIdAsSpUserId();

            if (useNameIdAsSpUserId)
            {
                if (authnResponseContext.NameIds.Count == 0)
                {
                    throw new SamlValidationException("Could not find NameId that would be used as UserId.");
                }

                samlUserData.UserId = this.authnResponseContext.NameIds.First().Value;
                samlUserData.Claims.Add(new Claim(ClaimTypes.NameIdentifier, samlUserData.UserId));
            }

            UserAttributeMapping userAttributeMapping = this.idpConfigurationProvider.GetUserAttributeMapping();

            foreach(Models.Xml.Attribute attribute in this.authnResponseContext.Attributes)
            {
                if (userAttributeMapping.Email != null && attribute.Name == userAttributeMapping.Email)
                {
                    samlUserData.Email = attribute.AttributeValues.First().Value;
                    samlUserData.Claims.Add(new Claim(ClaimTypes.Email, samlUserData.Email));
                }

                if (userAttributeMapping.FirstName != null && attribute.Name == userAttributeMapping.FirstName)
                {
                    samlUserData.FirstName = attribute.AttributeValues.First().Value;
                    samlUserData.Claims.Add(new Claim(ClaimTypes.GivenName, samlUserData.FirstName));
                }

                if (userAttributeMapping.LastName != null && attribute.Name == userAttributeMapping.LastName)
                {
                    samlUserData.LastName = attribute.AttributeValues.First().Value;
                    samlUserData.Claims.Add(new Claim(ClaimTypes.Surname, samlUserData.LastName));
                }

                if (userAttributeMapping.UserId != null && attribute.Name == userAttributeMapping.UserId && !useNameIdAsSpUserId)
                {
                    samlUserData.UserId = attribute.AttributeValues.First().Value;
                    samlUserData.Claims.Add(new Claim(ClaimTypes.Name, samlUserData.UserId));
                }
            }

            if (!samlUserData.UserId.IsNotNullOrWhitspace())
            {
                throw new SamlValidationException("User id could not be extracted from saml response. Check UseNameIdAsSpUserId property or UserId mapping.");
            }

            if (this.authnResponseContext.SessionInfos.Count > 0)
            {
                samlUserData.SessionInfo = this.authnResponseContext.SessionInfos.First();
                samlUserData.Claims.Add(new Claim(ClaimName.SessionIndex, samlUserData.SessionInfo.SessionIndex));
            }

            return samlUserData;
        }
    }
}
