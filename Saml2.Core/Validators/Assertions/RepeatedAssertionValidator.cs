using Saml2.Core.Models.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Validators.Assertions
{
    public interface IRepeatedAssertionValidator
    {
        Task Validate(Assertion assertion);
    }

    public class RepeatedAssertionValidator : IRepeatedAssertionValidator
    {

        public RepeatedAssertionValidator() 
        {
        }

        public async Task Validate(Assertion assertion)
        {
            
        }
    }
}
