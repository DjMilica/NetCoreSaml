namespace Saml2.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrWhitspace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidSamlId(this string value)
        {
            return value.IsNotNullOrWhitspace();
        }
    }
}
