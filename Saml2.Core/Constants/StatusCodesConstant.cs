using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Saml2.Core.Constants
{
    public class StatusCodeNameAndDetails
    {
        public string Name;
        public string Details;

        public StatusCodeNameAndDetails(string name, string details)
        {
            this.Name = name;
            this.Details = details;
        }
    }

    public class StatusCodesConstant
    {
        public static StatusCodeNameAndDetails SUCCESS = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:Success", "The request succeeded.");
        public static StatusCodeNameAndDetails REQUESTER = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:Requester", "The request could not be performed due to an error on the part of the requester.");
        public static StatusCodeNameAndDetails RESPONDER = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:Responder", "The request could not be performed due to an error on the part of the SAML responder or SAML authority.");
        public static StatusCodeNameAndDetails VERSION_MISMATCH = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:VersionMismatch", "The SAML responder could not process the request because the version of the request message was incorrect.");

        public static StatusCodeNameAndDetails AUTHN_FAILED = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:AuthnFailed", "The responding provider was unable to successfully authenticate the principal.");
        public static StatusCodeNameAndDetails INVALID_ATTR = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:InvalidAttrNameOrValue", "Unexpected or invalid content was encountered within a <saml:Attribute> or <saml:AttributeValue> element.");
        public static StatusCodeNameAndDetails INVALID_NAMEID_POLICY = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:InvalidNameIDPolicy", "The responding provider cannot or will not support the requested name identifier policy.");
        public static StatusCodeNameAndDetails NO_AUTHN_CONTEXT = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:NoAuthnContext", "The specified authentication context requirements cannot be met by the responder.");
        public static StatusCodeNameAndDetails NO_AVAILABLE_IDP = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:NoAvailableIDP", "None of the supported identity provider <Loc> elements in an <IDPList> can be resolved or none of the supported identity providers are available.");
        public static StatusCodeNameAndDetails NO_PASSIVE = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:NoPassive", "The responding provider cannot authenticate the principal passively, as has been requested.");
        public static StatusCodeNameAndDetails NO_SUPPORTED_IDP = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:NoSupportedIDP", "None of the identity providers in an <IDPList> are supported by the intermediary.");
        public static StatusCodeNameAndDetails PARTIAL_LOGOUT = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:PartialLogout", "Session authority is not able to propagate logout to all other session participants.");
        public static StatusCodeNameAndDetails PROXY_COUNT_EXCEEDED = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:ProxyCountExceeded", "Responding provider cannot authenticate the principal directly and it is not permitted to proxy the request furhter");
        public static StatusCodeNameAndDetails REQUEST_DENIED = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:RequestDenied", "The SAML responder or SAML authority is able to process the request but has chosen not to respond.This status code MAY be used when there is concern about the security context of the request message or the sequence of request messages received from a particular requester.");
        public static StatusCodeNameAndDetails REQUEST_UNSUPPORTED = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:RequestUnsupported", "The SAML responder or SAML authority does not support the request.");
        public static StatusCodeNameAndDetails REQUEST_VERSION_DEPRECATED = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:RequestVersionDeprecated", "The SAML responder cannot process any requests with the protocol version specified in the request.");
        public static StatusCodeNameAndDetails REQUEST_VERSION_TOO_HIGH = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:RequestVersionTooHigh", "The SAML responder cannot process the request because the protocol version specified in the request message is a major upgrade from the highest protocol version supported by the responder.");
        public static StatusCodeNameAndDetails REQUEST_VERSION_TOO_LOW = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:RequestVersionTooLow", "The SAML responder cannot process the request because the protocol version specified in the request message is too low.");
        public static StatusCodeNameAndDetails RESOURCE_NOT_RECOGNIZED = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:ResourceNotRecognized", "The resource value provided in the request message is invalid or unrecognized.");
        public static StatusCodeNameAndDetails TOO_MANY_RESPONSES = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:TooManyResponses", "The response message would contain more elements than the SAML responder is able to return.");
        public static StatusCodeNameAndDetails UNKNOWN_ATTR_PROFILE = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:UnknownAttrProfile", "An entity that has no knowledge of a particular attribute profile has been presented with an attribute drawn from that profile.");
        public static StatusCodeNameAndDetails UNKNOWN_PRINCIPAL = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:UnknownPrincipal", "The responding provider does not recognize the principal specified or implied by the request.");
        public static StatusCodeNameAndDetails UNSUPPORTED_BINDING = new StatusCodeNameAndDetails("urn:oasis:names:tc:SAML:2.0:status:UnsupportedBinding", "The SAML responder cannot properly fulfill the request using the protocol binding specified in the request.");
    
        public static string GetDetails(string name)
        {
            PropertyInfo[] properties = typeof(StatusCodesConstant).GetProperties();

            foreach(PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == typeof(StatusCodeNameAndDetails))
                {
                    StatusCodeNameAndDetails value = (StatusCodeNameAndDetails)propertyInfo.GetConstantValue();

                    if (value.Name == name)
                    {
                        return value.Details;
                    }

                }
            }

            return string.Empty;
        }
    
    }
}
