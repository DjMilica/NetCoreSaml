using Saml2.Core.Constants;
using System.ComponentModel;

namespace Saml2.Core.Enums
{
    public enum CorrespondenceType
    {
        [Description(SamlConstant.SamlRequest)]
        Request = 0,
        [Description(SamlConstant.SamlResponse)]
        Response = 1
    }
}
