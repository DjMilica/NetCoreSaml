using Saml2.Core.Constants;
using Saml2.Core.Enums;
using Saml2.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace Saml2.Core.Builders
{
    public interface ISigningUrlQueryBuilder
    {
        string Build(CorrespondenceType correspondenceType, string encodedSamlData, string relayState, SignatureAlgorithm signatureAlgorithm);
    }

    public class SigningUrlQueryBuilder : ISigningUrlQueryBuilder
    {
        public string Build(CorrespondenceType correspondenceType, string encodedSamlData, string relayState, SignatureAlgorithm signatureAlgorithm)
        {
            string encodedRelayState = relayState.IsNotNullOrWhitspace() ? $"&RelayState={WebUtility.UrlEncode(relayState)}" : "";

            return $"{correspondenceType.ToDescriptionString()}={WebUtility.UrlEncode(encodedSamlData)}{encodedRelayState}&SigAlg={WebUtility.UrlEncode(signatureAlgorithm.Url)}";
        }
    }
}
