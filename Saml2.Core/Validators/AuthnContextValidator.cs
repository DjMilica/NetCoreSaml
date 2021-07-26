using Saml2.Core.Errors;
using Saml2.Core.Extensions;
using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators
{
    public interface IAuthnContextValidator
    {
        Task Validate(AuthnContext authnContext);
    }

    public class AuthnContextValidator : IAuthnContextValidator
    {
        public async Task Validate(AuthnContext authnContext)
        {
            SamlValidationGuard.NotNull(
                authnContext,
                "AuthnContext should be defined as part of the AuthnStatement"
            );

            if (authnContext.AuthnContextClassRef == null 
                && authnContext.AuthnContextDecl == null 
                && authnContext.AuthnContextDeclRef == null
            )
            {
                throw new SamlValidationException("AuthnContext element MUST contain at least one AuthnContextClassRef, AuthnContextDecl or AuthnContextDeclRef element.");
            }

            if (authnContext.AuthnContextDecl != null && authnContext.AuthnContextDeclRef != null)
            {
                throw new SamlValidationException("AuthnContext should not have defined both AuthnContextDecl and AuthnContextDeclRef");
            }

            if (authnContext.AuthnContextClassRef != null && !authnContext.AuthnContextClassRef.Value.IsNotNullOrWhitspace())
            {
                throw new SamlValidationException("If AuthnContextClassRef is sent in AuthnContext, it should have some chars different than whitespace.");
            }

            if (authnContext.AuthnContextDecl != null && !authnContext.AuthnContextDecl.Value.IsNotNullOrWhitspace())
            {
                throw new SamlValidationException("If AuthnContextDecl is sent in AuthnContext, it should have some chars different than whitespace.");
            }

            if (authnContext.AuthnContextDeclRef != null && !authnContext.AuthnContextDeclRef.Value.IsNotNullOrWhitspace())
            {
                throw new SamlValidationException("If AuthnContextDeclRef is sent in AuthnContext, it should have some chars different than whitespace.");
            }
        }
    }
}
