using IdentityServer4.Models;
using IdentityServer4.Stores;
using Ids4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace Ids4Server.Extensions
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
            var client = gwlDb.AppClients.FirstOrDefault(a => a.ClientId == clientId);
            await Task.CompletedTask;

            if (client != null)
            {

                var scopes = client.AllowedScopes.Split(",").ToList();
                scopes.Add(StandardScopes.OfflineAccess);

                var grantTypes = client.GrantTypes.Split(",").ToList();

                return new Client()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    ClientSecrets = new[] { new Secret(client.Secret.Sha256()) },
                    AllowedGrantTypes = grantTypes,
                    AllowOfflineAccess = true,
                    AllowedScopes = scopes,
                    RequirePkce = false,
                    RedirectUris = new[] { "https://www.baidu.com" }
                };
            }
            return null;
        }
    }
}
