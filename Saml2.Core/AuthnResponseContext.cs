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
        public List<SubjectConfirmationData> bearerSubjectConfirmationsData = new List<SubjectConfirmationData>();
        public List<NameId> NameIds = new List<NameId>();
        public List<Attribute> Attributes = new List<Attribute>();
    }
}
