using Microsoft.Extensions.Logging;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Factories
{
    public interface IAuthnRequestFactory
    {
    }

    public class AuthnRequestFactory: IAuthnRequestFactory
    {
        private readonly ILogger logger;

        public AuthnRequestFactory(
            ILogger<AuthnRequestFactory> logger
        )
        {
            this.logger = logger;
        }
    }
}
