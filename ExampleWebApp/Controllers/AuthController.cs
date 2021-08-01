using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExampleWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExampleWebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly ILogger logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        // use temp data because you want to pass error message to an error page
        [TempData]
        public string ErrorMessage { get; set; }

        public AuthController(
            ILogger<AuthController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.userManager = userManager;
        }

        //[Route("login")]
        [HttpGet]
        public IActionResult LogIn() => View();

        //[Route("login/{provider}")]
        [HttpPost]
        public IActionResult LogIn(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            string redirectUrl = Url.Action(nameof(LoginCallback), "Auth", new { externalProvider = provider, returnUrl });
            AuthenticationProperties properties = 
                this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(string externalProvider, string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(LogIn));
            }

            AuthenticateResult authResult = await HttpContext.AuthenticateAsync();

            //ExternalLoginInfo info = await this.signInManager.GetExternalLoginInfoAsync();
            //if (info == null)
            //{
            //    return RedirectToAction(nameof(LogIn));
            //}

            if (!authResult.Succeeded)
            {
                return RedirectToAction(nameof(LogIn));
            }

            ClaimsPrincipal principal = authResult.Principal;
            AuthenticationProperties properties = authResult.Properties ?? new AuthenticationProperties();
            string loginProvider = externalProvider;
            string providerKey = authResult.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

            // Sign in the user with this external login provider if the user already has a login.
            Microsoft.AspNetCore.Identity.SignInResult result = 
                await this.signInManager.ExternalLoginSignInAsync(
                    loginProvider, 
                    providerKey, 
                    isPersistent: false
                );
            
            if (result.Succeeded)
            {
                this.logger.LogInformation("User logged in with {Name} provider.", loginProvider);
                return RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, make an account for it.
            var email = principal.FindFirstValue(ClaimTypes.Email);
            var user = new ApplicationUser { UserName = email, Email = email };
            var createUserResult = await this.userManager.CreateAsync(user);
            if (createUserResult.Succeeded)
            {
                string displayName = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).FirstOrDefault(s => s.Name == externalProvider).DisplayName;
                UserLoginInfo userLoginInfo = new UserLoginInfo(loginProvider, providerKey, displayName);
                createUserResult = await this.userManager.AddLoginAsync(user, userLoginInfo);
                if (createUserResult.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    this.logger.LogInformation("User created an account using {Name} provider.", loginProvider);
                    return RedirectToLocal(returnUrl);
                }
            }

            AddErrors(createUserResult);
            return RedirectToAction(nameof(LogIn));
        }

        //[Route("logout")]
        public async Task<IActionResult> LogOut(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToLocal(returnUrl);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
