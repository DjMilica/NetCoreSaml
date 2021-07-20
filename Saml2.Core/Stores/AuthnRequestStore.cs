using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Stores
{
    public interface IAuthnRequestStore
    {
        Task Insert(string authnRequestId);
        Task Remove(string authnRequestId);
        Task<bool> CheckIfExists(string authnRequestId);
    }

    public class AuthnRequestStore: IAuthnRequestStore
    {
        private readonly IDistributedCache distributedCache;

        public AuthnRequestStore(
            IDistributedCache distributedCache
        )
        {
            this.distributedCache = distributedCache;
        }

        public async Task Insert(string authnRequestId)
        {
            await this.distributedCache.SetAsync(this.GetKey(authnRequestId), this.GetDefaultValue(authnRequestId));
        }

        public async Task Remove(string authnRequestId)
        {
            await this.distributedCache.RemoveAsync(this.GetKey(authnRequestId));
        }

        public async Task<bool> CheckIfExists(string authnRequestId)
        {
            byte[] existingValue = await this.distributedCache.GetAsync(this.GetKey(authnRequestId));

            if (existingValue == null || !existingValue.SequenceEqual(this.GetDefaultValue(authnRequestId)))
            {
                return false;
            }

            return true;
        }

        private string GetKey(string id)
        {
            return $"SAML-authn-request-id-{id}";
        }

        private byte[] GetDefaultValue(string authnRequestId)
        {
            return Encoding.UTF8.GetBytes(authnRequestId);
        }
    }
}
