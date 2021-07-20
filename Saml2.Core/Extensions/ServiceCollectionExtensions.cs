using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Saml2.Core.Builders;
using Saml2.Core.Configuration;
using Saml2.Core.Encoders;
using Saml2.Core.Errors;
using Saml2.Core.Factories;
using Saml2.Core.Handlers;
using Saml2.Core.Helpers;
using Saml2.Core.Providers;
using Saml2.Core.Services;
using Saml2.Core.Stores;
using Saml2.Core.Validators;
using System.Linq;

namespace Saml2.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSamlServices(this IServiceCollection services)
        {
            services.AddOptions();

            services.AddScoped(resolver => resolver.GetRequiredService<IOptions<SamlConfiguration>>().Value);

            if (!services.Any(x => x.ServiceType == typeof(IHttpContextAccessor)))
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }

            if (!services.Any(x => x.ServiceType == typeof(IDistributedCache)))
            {
                throw new SamlInternalException("Application should register some distributed cache for SAML library successful execution.");
            }

            services.AddTransient<IAuthnRequestStore, AuthnRequestStore>();

            services.AddTransient<ISpConfigurationProvider, SpConfigurationProvider>();

            services.AddTransient<ISerializeXmlService, SerializeXmlService>();

            services.AddScoped<IIdpConfigurationProviderFactory, IdpConfigurationProviderFactory>();

            services.AddTransient<IAuthnRequestFactory, AuthnRequestFactory>();

            services.AddTransient<IAuthnRequestService, AuthnRequestService>();

            services.AddTransient<IAuthnRequestXmlProvider, AuthnRequestXmlProvider>();

            services.AddTransient<ISamlSchemeGenerator, SamlSchemeGenerator>();

            services.AddTransient<ISamlRedirectDataFactory, SamlRedirectDataFactory>();

            services.AddTransient<ISamlEncoder, SamlEncoder>();

            services.AddTransient<ISigningUrlQueryBuilder, SigningUrlQueryBuilder>();

            services.AddTransient<ISamlSignatureHelper, SamlSignatureHelper>();

            services.AddTransient<IRsaKeyProvider, RsaKeyProvider>();

            services.AddTransient<IAuthnResponseHandler, AuthnResponseHandler>();

            services.AddScoped<AuthnResponseContext>();
            services.AddTransient<AuthResponseAttributeValidator>();
            services.AddTransient<AuthnResponseIssuerValidator>();
            services.AddTransient<INameIdFormatValidator, NameIdFormatValidator>();
            services.AddTransient<AuthnResponseStatusValidator>();
            services.AddTransient<AuthnResponseSignatureValidator>();
            services.AddTransient<AuthnResponseDecryptAssertionValidator>();
            services.AddTransient<AuthnResponseAssertionSignatureValidator>();
            services.AddTransient<AuthnResponseAssertionValidator>();
            services.AddTransient<IAuthnResponseValidatorListProvider, AuthnResponseValidatorListProvider>();

            return services;
        }
    }
}
