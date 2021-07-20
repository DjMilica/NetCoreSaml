using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;

namespace Saml2.Core
{
    public class AuthnResponseContext
    {
        public string StringifiedResponse;
        public AuthnResponse Response;
        public IIdpConfigurationProvider idpConfigurationProvider;
    }
}
