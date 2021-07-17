using Saml2.Core.Constants;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Saml2.Core.Providers
{

    public interface IAuthnRequestXmlProvider
    {
        string Get(AuthnRequest request);
    }

    public class AuthnRequestXmlProvider: IAuthnRequestXmlProvider
    {
        public string Get(AuthnRequest request)
        {
            string assertionConsumerServiceUrlString = 
                request.AssertionConsumerServiceUrl.IsNotNullOrWhitspace() 
                ? $" AssertionConsumerServiceURL=\"{request.AssertionConsumerServiceUrl}\"" : "";

            string protocolBindingString = 
                request.ProtocolBinding.IsNotNullOrWhitspace()
                ? $" ProtocolBinding=\"{request.ProtocolBinding}\"" : "";

            string signatureTag =
                request.Signature != null
                ? request.Signature.GetXml() : "";

            string requestXml = $"<samlp:AuthnRequest" +
                $" xmlns:samlp=\"{NamespaceConstant.Samlp}\"" +
                $" ID=\"{request.Id}\"" +
                $" Version=\"{request.Version}\"" +
                $" IssueInstant=\"{request.IssueInstant.ToIso8601()}\"" +
                $" Destination=\"{ request.Destination }\"" +
                $"{protocolBindingString}" +
                $"{assertionConsumerServiceUrlString}" +
                ">" +
                $"<saml:Issuer xmlns:saml=\"{NamespaceConstant.Saml}\">{request.Issuer.Value}</saml:Issuer>" +
                $"{signatureTag}" +
                "</samlp:AuthnRequest>";

            return requestXml;
        }
    }
}
