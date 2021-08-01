using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Models
{
    public class RelayStateData
    {
        public string ReturnUrl { get; set; }
        public AuthenticationProperties AuthenticationProperties { get; set; }
    }
}
