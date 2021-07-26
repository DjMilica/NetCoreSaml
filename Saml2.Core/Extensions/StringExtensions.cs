namespace Saml2.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrWhitspace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool HasOnlyWhitespaceChars(this string value)
        {
            string trimmedValue = value.Trim();

            return trimmedValue == string.Empty;
        }

        public static bool IsValidSamlId(this string value)
        {
            return value.IsNotNullOrWhitspace();
        }
    }
}
