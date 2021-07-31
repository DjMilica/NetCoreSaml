using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Models
{
    public class SamlUserData
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
