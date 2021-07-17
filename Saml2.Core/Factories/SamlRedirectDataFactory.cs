using Saml2.Core.Builders;
using Saml2.Core.Constants;
using Saml2.Core.Encoders;
using Saml2.Core.Enums;
using Saml2.Core.Extensions;
using Saml2.Core.Helpers;
using Saml2.Core.Models;
using Saml2.Core.Models.Xml;
using Saml2.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saml2.Core.Factories
{
    public interface ISamlRedirectDataFactory
    {
        SamlRedirectData Create(string idpUrl, string xmlRequest, CorrespondenceType correspondenceType, bool sign, string relayState = "", SignatureAlgorithm signatureAlgorithm = null);
    }

    public class SamlRedirectDataFactory: ISamlRedirectDataFactory
    {
        private readonly ISamlEncoder samlEncoder;
        private readonly ISigningUrlQueryBuilder signingUrlQueryBuilder;
        private readonly ISamlSignatureHelper samlSignatureHelper;

        public SamlRedirectDataFactory(
            ISamlEncoder samlEncoder,
            ISigningUrlQueryBuilder signingUrlQueryBuilder,
            ISamlSignatureHelper samlSignatureHelper
        )
        {
            this.samlEncoder = samlEncoder;
            this.signingUrlQueryBuilder = signingUrlQueryBuilder;
            this.samlSignatureHelper = samlSignatureHelper;
        }

        public SamlRedirectData Create(string idpUrl, string xmlRequest, CorrespondenceType correspondenceType, bool sign, string relayState = "", SignatureAlgorithm signatureAlgorithm = null)
        {
            string encodedRelayState =
                relayState.IsNotNullOrWhitspace() ? this.samlEncoder.DeflateAndBase64Encode(relayState) : null;

            string encodedData = this.samlEncoder.DeflateAndBase64Encode(xmlRequest);

            signatureAlgorithm ??= SignatureAlgorithmConstants.RsaSha256;

            string signature = null;

            if (sign)
            {
                string dataToSign = this.signingUrlQueryBuilder.Build(correspondenceType, encodedData, encodedRelayState, signatureAlgorithm);
                signature = this.samlSignatureHelper.Build(signatureAlgorithm, dataToSign);
            }


            return new SamlRedirectData(
                idpUrl,
                encodedData,
                correspondenceType,
                encodedRelayState,
                signatureAlgorithm.Url,
                signature
            );
        }
    }
}
