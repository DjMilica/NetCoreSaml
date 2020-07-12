using System;

namespace Saml2.Core.Errors
{
    public class SamlValidationException : SamlInternalException
    {
        public SamlValidationException(string message, params string[] parameters)
            : base(string.Format(message, parameters))
        {
        }

        public SamlValidationException(string message, Exception error, params string[] parameters)
            : base(string.Format(message, parameters), error)
        {
        }
    }

    public class SamlValidationErrors
    {
        public const string IdAttributeShouldNotBeNullOrUndefined = "Id attribute of {0} element should not be null or empty.";
        public const string TimeAttributeShouldNotBeNull = "Time attribute {0} of {1} element should not be null.";
        public const string IssueInstantShouldNotBeInFuture = "IssueInstant attribute of {1} element should not be in the future.";
        public const string VersionMissmatch = "Received {0} version, but only supporting {1} version.";
    }
}