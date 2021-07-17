using System;

namespace Saml2.Core.Extensions
{
    public static class DateExtenstions
    {
        public static string ToIso8601(this DateTime value)
        {
            return value.ToString("o");
        }
    }
}
