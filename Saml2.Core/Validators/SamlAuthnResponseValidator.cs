
using Saml2.Core.Models.Xml;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public interface ISamlAuthnResponseValidator
    {
        Task Validate(AuthnResponse data);
    }

    public abstract class BaseAuthnResponseValidator : ISamlAuthnResponseValidator
    {
        protected readonly AuthnResponseContext authnResponseContext;

        public BaseAuthnResponseValidator(
            AuthnResponseContext authnResponseContext
        )
        {
            this.authnResponseContext = authnResponseContext;
        }

        public abstract Task Validate(AuthnResponse data);
    }
}
