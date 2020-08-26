using Microsoft.AspNetCore.Mvc;
using Saml2.Core.Configuration;
using Saml2.Core.Services;
using System.Threading.Tasks;

namespace ExampleWebApp.Controllers
{
    public class SamlController: Controller
    {
        private readonly ISamlSchemeGenerator samlSchemeGenerator;

        public SamlController(
            ISamlSchemeGenerator samlSchemeGenerator
        )
        {
            this.samlSchemeGenerator = samlSchemeGenerator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Remove(string scheme)
        {
            this.samlSchemeGenerator.Remove(scheme);

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(string scheme, string entityId)
        {
            IdentityProviderConfiguration identityProviderConfiguration =
                new IdentityProviderConfiguration
                {
                    EntityId = entityId
                };

            await this.samlSchemeGenerator.AddOrUpdate(scheme, identityProviderConfiguration);
            
            return Redirect("/");
        }
    }
}
