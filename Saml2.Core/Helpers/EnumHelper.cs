using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using System;

namespace Saml2.Core.Helpers
{
    public static class EnumHelper
    {
        public static T ParseEnumFromDescription<T>(string value) where T : Enum
        {
            foreach (T n in Enum.GetValues(typeof(T)))
            {
                if (n.ToDescriptionString() == value)
                {
                    return (T)n;
                }
            }

            throw new SamlInternalException($"Sent value {value} cannot be parsed to enum {typeof(T)}.");
        }
    }
}
