using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;

namespace AuthenticationServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("UserAPI")
               
            };
        }

       
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "SuperAdmin",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    Claims = new[]
                    {
                        new Claim("Role", "Admin"),
                    },
                    ClientClaimsPrefix = "",
                    AllowedScopes = { "UserAPI" }
                }
            };

        }
        
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = JwtClaimTypes.Role,
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                }
            };

        }
    }
}

