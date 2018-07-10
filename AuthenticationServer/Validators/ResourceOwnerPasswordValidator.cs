using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using System.Security.Claims;
using IdentityModel;


namespace AuthenticationServer.Validators
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUsersRepository usersRepository;

        public ResourceOwnerPasswordValidator(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its login)
                var user = usersRepository.FindUserAsync(context.UserName);
                if (user != null)
                {
                    //check if password match - remember to hash password if stored as hash in db
                    if (user.Password == context.Password)
                    {
                        //set the result
                        context.Result = new GrantValidationResult(
                            subject: user.Id.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
        public static IEnumerable<Claim> GetUserClaims(BaseUser user)
        {
            var userRepository = new UsersRepository();
            return new List<Claim>
            {
                new Claim("user_id", user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.Name) ? (user.Name) : "")),

                //roles
                new Claim(JwtClaimTypes.Role, userRepository.GetRole(user.Roles_ID).Name)
            };
        }
    }
}
