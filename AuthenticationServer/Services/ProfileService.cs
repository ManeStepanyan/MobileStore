using System;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationServer.Validators;
using IdentityServer4.Models;
using IdentityServer4.Services;
using BusinessLayer;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;

namespace AuthenticationServer.Services
{
    public class ProfileService
    {
        private IUsersRepository usersRepository;

        public ProfileService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    var user = usersRepository.FindUserAsync(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        var claims = GetUserClaims(user);
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else
                {
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                    {
                        var user = usersRepository.FindUserAsync(int.Parse(userId.Value));

                        if (user != null)
                        {
                            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
                throw ex;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                {
                    var user = usersRepository.FindUserAsync(int.Parse(userId.Value));

                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            context.IsActive = user.IsActive;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<Claim> GetUserClaims(BaseUser user)
        {
            var userRepository = new UsersRepository();
            return new List<Claim>
            {
                new Claim("user_id", user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.Name) ? (user.Name) : "")),

                new Claim(JwtClaimTypes.Role, userRepository.GetRole(user.Roles_ID).Name)
            };
        }
    }
}
