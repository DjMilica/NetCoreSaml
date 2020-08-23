using Microsoft.AspNetCore.Authentication;

namespace Saml2.Core.Configuration
{
    public class SamlAuthenticationOptions: AuthenticationSchemeOptions
    {
        public IdentityProviderConfiguration IdentityProviderConfiguration { get; set; }
    }
}
