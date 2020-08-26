using Saml2.Core.Constants;
using System.ComponentModel;

namespace Saml2.Core.Enums
{
    public enum BindingType
    {
        [Description(SamlConstant.BindingTypeHttpRedirect)]
        HttpRedirect = 0,
        [Description(SamlConstant.BindingTypeHttpPost)]
        HttpPost = 1
    }
}
