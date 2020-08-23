using Microsoft.AspNetCore.Authentication;
using Saml2.Core.Configuration;
using Saml2.Core.Constants;
using Saml2.Core.Handlers;
using System;

namespace Saml2.Core.Extensions
{
    public static class AuthenticationBuilderExtension
    {
        public static AuthenticationBuilder AddSaml(this AuthenticationBuilder builder, Action<SamlAuthenticationOptions> options)
        {
            return builder.AddSaml(AuthenticationSchemeConstant.Name, options);
        }

        public static AuthenticationBuilder AddSaml(this AuthenticationBuilder builder, string authenticationSchemeName, Action<SamlAuthenticationOptions> options)
        {
            return builder.AddSaml(authenticationSchemeName, AuthenticationSchemeConstant.DisplayName, options);
        }

        public static AuthenticationBuilder AddSaml(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SamlAuthenticationOptions> options)
        {
            return builder.AddScheme<SamlAuthenticationOptions, SamlAuthenticationHandler>(authenticationScheme, displayName, options);
        }
    }
}
