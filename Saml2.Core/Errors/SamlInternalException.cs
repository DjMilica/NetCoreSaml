using System;

namespace Saml2.Core.Errors
{
    public class SamlInternalException: Exception
    {
        public SamlInternalException(string message)
            : base(message)
        {
        }

        public SamlInternalException(string message, Exception error)
            : base(message, error)
        {
        }
    }
}
