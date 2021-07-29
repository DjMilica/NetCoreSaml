using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saml2.Core.Stores
{
    public interface IAssertionStore
    {
        Task Insert(string assertionId, DateTime expirationDate);
        Task Remove(string assertionId);
        Task<bool> CheckIfExists(string assertionId);
    }

    public class AssertionStore : IAssertionStore
    {
        private readonly IDistributedCache distributedCache;

        public AssertionStore(
            IDistributedCache distributedCache
        )
        {
            this.distributedCache = distributedCache;
        }

        public async Task Insert(string assertionId, DateTime expirationDate)
        {
            DateTimeOffset expirationOffset = new DateTimeOffset(expirationDate);

            await this.distributedCache.SetAsync(
                this.GetKey(assertionId), 
                this.GetDefaultValue(assertionId), 
                new DistributedCacheEntryOptions() { AbsoluteExpiration = expirationOffset }
            );
        }

        public async Task Remove(string assertionId)
        {
            await this.distributedCache.RemoveAsync(this.GetKey(assertionId));
        }

        public async Task<bool> CheckIfExists(string assertionId)
        {
            byte[] existingValue = await this.distributedCache.GetAsync(this.GetKey(assertionId));

            if (existingValue == null || !existingValue.SequenceEqual(this.GetDefaultValue(assertionId)))
            {
                return false;
            }

            return true;
        }

        private string GetKey(string id)
        {
            return $"SAML-assertion-id-{id}";
        }

        private byte[] GetDefaultValue(string assertionId)
        {
            return Encoding.UTF8.GetBytes(assertionId);
        }
    }
}
