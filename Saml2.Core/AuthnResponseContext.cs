using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System.Collections.Generic;

namespace Saml2.Core
{
    public class AuthnResponseContext
    {
        public string StringifiedResponse;
        public AuthnResponse Response;
        public IIdpConfigurationProvider idpConfigurationProvider;
        public List<SubjectConfirmationData> bearerSubjectConfirmations = new List<SubjectConfirmationData>();
    }
}
