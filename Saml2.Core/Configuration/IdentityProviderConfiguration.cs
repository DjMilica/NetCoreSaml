using Saml2.Core.Enums;

namespace Saml2.Core.Configuration
{
    public class IdentityProviderConfiguration
    {
        public string EntityId { get; set; }

        public BindingType? AuthnRequestBinding { get; set; }

        public BindingType? LogoutRequestBinding { get; set; }

        public int? MillisecondsSkew { get; set; }

        public bool? UseNameIdAsSpUserId { get; set; }
    }
}
