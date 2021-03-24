using IdentityServer4.Models;
using IdentityServer4.Stores;
using Ids4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ids4Server
{
    public class DbClientStore : IClientStore
    {
        private readonly GwlDbContext gwlDb;

        public DbClientStore(GwlDbContext gwlDb)
        {
            this.gwlDb = gwlDb;
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = gwlDb.AppClients.FirstOrDefault();
            await Task.CompletedTask;
            if (client != null)
            {
                return new Client()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    ClientSecrets = new[] { new Secret("123") },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new[] { "UserApi" }
                };
            }
            return null;
        }
    }
}
