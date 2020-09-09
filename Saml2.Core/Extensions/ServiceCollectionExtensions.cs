using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Factories;
using Saml2.Core.Providers;
using Saml2.Core.Services;

namespace Saml2.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSamlServices(this IServiceCollection services)
        {
            services.AddOptions();

            services.AddScoped(resolver => resolver.GetRequiredService<IOptions<SamlConfiguration>>().Value);

            services.AddTransient<ISpConfigurationProvider, SpConfigurationProvider>();

            services.AddTransient<ISerializeXmlService, SerializeXmlService>();

            services.AddScoped<IIdpConfigurationProviderFactory, IdpConfigurationProviderFactory>();

            services.AddTransient<IAuthnRequestFactory, AuthnRequestFactory>();

            services.AddTransient<IAuthnRequestService, AuthnRequestService>();

            services.AddTransient<ISamlSchemeGenerator, SamlSchemeGenerator>();

            return services;
        }
    }
}
