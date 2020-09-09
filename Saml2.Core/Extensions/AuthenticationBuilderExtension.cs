using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
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
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IValidateOptions<SamlAuthenticationOptions>, SamlAuthenticationOptionValidation>());

            builder.Services.Configure(authenticationScheme, options);
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<SamlAuthenticationOptions>, SamlAuthenticationOptionsPostConfigure>());
            
            AuthenticationBuilder authenticationBuilder = builder.AddScheme<SamlAuthenticationOptions, SamlAuthenticationHandler>(authenticationScheme, displayName, options);

            return authenticationBuilder;
        }
    }

    public class SamlAuthenticationOptionValidation : IValidateOptions<SamlAuthenticationOptions>
    {
        public ValidateOptionsResult Validate(string name, SamlAuthenticationOptions options)
        {
            if (options.IdentityProviderConfiguration == null)
            {
                return ValidateOptionsResult.Fail("Identity provider configuration is not defined in authentication options");
            }

            if (options.IdentityProviderConfiguration.EntityId == null)
            {
                return ValidateOptionsResult.Fail("Identity provider entity id is not defined in authentication options.");
            }


            return ValidateOptionsResult.Success;
        }
    }
}
