using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Models
{
    public class SamlResolvedFromResponseSessionInfo
    {
        public string SessionIndex { get; set; }
        public DateTime SessionNotOnOrAfter { get; set; }
    }
}
