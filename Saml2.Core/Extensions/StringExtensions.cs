namespace Saml2.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsValidSamlId(this string value)
        {
            return value.IsNotNullOrEmpty();
        }
    }
}
