using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Saml2.Core.Configuration;
using Saml2.Core.Handlers;
using System.Threading.Tasks;

namespace Saml2.Core.Services
{
    public interface ISamlSchemeGenerator
    {
        Task AddOrUpdate(string scheme, IdentityProviderConfiguration identityProviderConfiguration);
        void Remove(string scheme);
    }

    public class SamlSchemeGenerator: ISamlSchemeGenerator
    {
        private readonly IAuthenticationSchemeProvider schemeProvider;
        private readonly IOptionsMonitorCache<SamlAuthenticationOptions> optionsCache;

        public SamlSchemeGenerator(
            IAuthenticationSchemeProvider schemeProvider,
            IOptionsMonitorCache<SamlAuthenticationOptions> optionsCache
        )
        {
            this.schemeProvider = schemeProvider;
            this.optionsCache = optionsCache;
        }

        public void Remove(string scheme)
        {
            this.schemeProvider.RemoveScheme(scheme);
            this.optionsCache.TryRemove(scheme);
        }


        public async Task AddOrUpdate(string scheme, IdentityProviderConfiguration identityProviderConfiguration)
        {
            if (await this.schemeProvider.GetSchemeAsync(scheme) == null)
            {
                this.schemeProvider.AddScheme(new AuthenticationScheme(scheme, scheme, typeof(SamlAuthenticationHandler)));
            }
            else
            {
                this.optionsCache.TryRemove(scheme);
            }

            this.optionsCache.TryAdd(
                scheme,
                new SamlAuthenticationOptions { IdentityProviderConfiguration = identityProviderConfiguration }
            );
        }
    }
}
