
namespace Saml2.Core.Errors
{
    public class SamlValidationGuard
    {
        public static void NotNull(object data, string message, params string[] parameters)
        {
            if (data == null)
            {
                throw new SamlValidationException(message, parameters);
            }
        }

        public static void NotTrue(bool data, string message, params string[] parameters)
        {
            if (!data)
            {
                throw new SamlValidationException(message, parameters);
            }
        }
    }
}
