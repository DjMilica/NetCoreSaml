using Saml2.Core.Enums;
using Saml2.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Saml2.Core.Models
{
    public class SamlRedirectData
    {
        private string signingAlgorithm;
        private string signature;
        private CorrespondenceType correspondenceType;
        private string idpUrl;
        private string encodedData;
        private string relayState;


        public SamlRedirectData(
            string idpUrl,
            string encodedData,
            CorrespondenceType correspondenceType,
            string relayState = "",
            string signingAlgorithm = "",
            string signature = ""
        )
        {
            this.idpUrl = idpUrl;
            this.signature = signature;
            this.correspondenceType = correspondenceType;
            this.relayState = relayState;
            this.signingAlgorithm = signingAlgorithm;
            this.encodedData = encodedData;
        }


        public string ToRedirectUrl()
        {
            string redirectUrl = $"{this.idpUrl}?{this.correspondenceType.ToDescriptionString()}={WebUtility.UrlEncode(this.encodedData)}";

            if (this.relayState.IsNotNullOrWhitspace())
            {
                redirectUrl = $"{redirectUrl}&RelayState={WebUtility.UrlEncode(this.relayState)}";
            }

            if (this.signingAlgorithm.IsNotNullOrWhitspace() && this.signature.IsNotNullOrWhitspace())
            {
                redirectUrl = $"{redirectUrl}&SigAlg={WebUtility.UrlEncode(this.signingAlgorithm)}&Signature={WebUtility.UrlEncode(this.signature)}";
            }

            return redirectUrl;
        }
    }
}
