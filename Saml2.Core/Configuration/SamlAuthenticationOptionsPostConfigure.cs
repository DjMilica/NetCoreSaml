using Microsoft.Extensions.Options;
using Saml2.Core.Errors;

namespace Saml2.Core.Configuration
{
    class SamlAuthenticationOptionsPostConfigure: IPostConfigureOptions<SamlAuthenticationOptions>
    {
        public SamlAuthenticationOptionsPostConfigure()
        {
        }

        public void PostConfigure(string name, SamlAuthenticationOptions options)
        {
            //if (options.IdentityProviderConfiguration == null)
            //{
            //    throw new SamlInternalException("Identity provider configuration is not defined in authentication options");
            //}

            //if (options.IdentityProviderConfiguration.EntityId == null)
            //{
            //    throw new SamlInternalException("Identity provider entity id is not defined in authentication options.");
            //}
        }
    }
}
