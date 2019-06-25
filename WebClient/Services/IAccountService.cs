using System.Security.Claims;

namespace WebClient.Services
{
    public interface IAccountService
    {
        bool IsSignedIn(ClaimsPrincipal principal);
        string GetUserName(ClaimsPrincipal principal);
    }
}

