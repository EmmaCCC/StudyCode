using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ids4Server
{
    public class Ids4MemoryDatas
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("UserApi","UserService")
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
               new Client()
               {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "college",
                    ClientSecrets = new [] { new Secret("secret") },
                    AllowedScopes = new []{ "UserApi" }
               }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
             {
                 new ApiScope("UserApi")
             };
        }
    }
}
