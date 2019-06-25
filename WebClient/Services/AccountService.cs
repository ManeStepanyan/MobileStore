using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebClient.Services
{
    public class AccountService:IAccountService
    {
        private HttpContext httpContext;
        public AccountService(IHttpContextAccessor diContextAccessor)
        {
            httpContext = diContextAccessor.HttpContext;
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return principal?.Identities != null &&
                   principal.Identities.Any(i => i.AuthenticationType == TokenValidationParameters.DefaultAuthenticationType);
        }


        public string GetUserName(ClaimsPrincipal principal)
        {
            string name = principal.FindFirstValue(ClaimTypes.Name);

            if (String.IsNullOrEmpty(name))
                name = principal.FindFirstValue("name");

            // TODO load persisted user data via subject id, if needed

            return name;
        }
    }
}
