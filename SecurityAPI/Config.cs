using System.Collections.Generic;
using IdentityServer4.Models;

namespace SecurityAPI
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
    }
}
