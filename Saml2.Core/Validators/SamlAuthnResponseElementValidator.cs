
namespace Saml2.Core.Validators
{
    public interface ISamlAuthnResponseElementValidator<T>
    {
        void Validate(T data, AuthnResponseContext context);
    }
}
