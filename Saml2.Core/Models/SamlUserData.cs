using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Saml2.Core.Models
{
    public class SamlUserData
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public SamlResolvedFromResponseSessionInfo SessionInfo { get; set; }
        public List<Claim> Claims { get; set; }

        public SamlUserData()
        {
            this.Claims = new List<Claim>();
        }
    }
}
