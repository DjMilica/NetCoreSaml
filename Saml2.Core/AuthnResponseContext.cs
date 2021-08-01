using Saml2.Core.Models;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System.Collections.Generic;

namespace Saml2.Core
{
    public class AuthnResponseContext
    {
        public string StringifiedResponse { get; set; }
        public AuthnResponse Response { get; set; }
        public IIdpConfigurationProvider IdpConfigurationProvider { get; set; }
        public List<SubjectConfirmationData> BearerSubjectConfirmationsData { get; set; }
        public List<NameId> NameIds { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<SamlResolvedFromResponseSessionInfo> SessionInfos { get; set; }

        public AuthnResponseContext()
        {
            this.BearerSubjectConfirmationsData = new List<SubjectConfirmationData>();
            this.NameIds = new List<NameId>();
            this.Attributes = new List<Attribute>();
            this.SessionInfos = new List<SamlResolvedFromResponseSessionInfo>();
        }
    }
}
