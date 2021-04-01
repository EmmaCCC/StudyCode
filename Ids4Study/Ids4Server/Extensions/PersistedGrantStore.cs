using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ids4Server.Extensions
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            var grant = new PersistedGrant()
            {
                 ClientId = "9420bb7d-4001-4628-b720-4cb72f16b249",
                  CreationTime = DateTime.Now,
                   ConsumedTime = DateTime.Now,
                     Description = "第三方授权",
                      Expiration = DateTime.Now.AddDays(1),
                       Key = key,
                        SessionId = "12313213",
                         SubjectId = "88888",
                          Type = "authorization_code"
                           

            };

            grant.Data = JsonSerializer.Serialize(grant);
            return Task.FromResult(grant);
        }

        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            return Task.CompletedTask;
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            throw new NotImplementedException();
        }
    }
}
