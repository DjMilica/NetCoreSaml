using Saml2.Core.Enums;
using Saml2.Core.Models;

namespace Saml2.Core.Configuration
{
    public class IdentityProviderConfiguration
    {
        public string EntityId { get; set; }

        public BindingType? AuthnRequestBinding { get; set; }

        public BindingType? LogoutRequestBinding { get; set; }

        public bool? UseNameIdAsSpUserId { get; set; }

        public string HttpPostSingleSignOnService { get; set; }

        public string HttpRedirectSingleSignOnService { get; set; }

        public string HttpPostSingleLogoutService { get; set; }

        public string HttpRedirectSingleLogoutService { get; set; }

        public bool WantAuthnRequestsSigned { get; set; }

        public string PublicKeyPath { get; set; }

        public UserAttributeMapping UserAttributeMapping { get; set; }
    }
}
