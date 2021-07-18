
using Saml2.Core.Models.Xml;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public interface ISamlAuthnResponseValidator
    {
        Task Validate(AuthnResponse data);
    }
}
