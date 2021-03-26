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
            var list = new List<string>();
            //list.AddRange(GrantTypes.ResourceOwnerPasswordAndClientCredentials);
            list.Add(CustomGrantTypes.WeChat);
            list.Add(CustomGrantTypes.Sms);
            if (client != null)
            {

                var scopes = client.AllowedScopes.Split(",").ToList();
                scopes.Add(StandardScopes.OfflineAccess);

                return new Client()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    ClientSecrets = new[] { new Secret(client.Secret.Sha256()) },
                    AllowedGrantTypes = list,
                    AllowOfflineAccess = true,
                    AllowedScopes = scopes
                };
            }
            return null;
        }
    }
}
