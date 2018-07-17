using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityModel;
using AuthenticationServer.UsersRepository;
using Cryptography;

namespace AuthenticationServer.Validators
{
    /// <summary>
    /// Resource owner password validatpr
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// User repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructs new instance of 
        /// <see cref="ResourceOwnerPasswordValidator"/> with the given user repoistory.
        /// </summary>
        /// <param name="userRepository">User Repositor</param>
        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Validates context
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>Validation task.</returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                // getting user
                var user = await this._userRepository.FindAsync(context.UserName);
                var del = user.Password;
                // checking password
                if (user != null)
                {
                    // if password is ok set
                    if (user.Password == MyCryptography.Encrypt(context.Password)) //
                    {
                        context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: "custom",
                        claims: GetUserClaims(user));
                        return;
                    }

                    // othwerwise construct error response
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                // message about non-existing user
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            // catching exception
            catch (Exception)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        /// <summary>
        /// Constructs claims with the given user.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Claims</returns>
        public static Claim[] GetUserClaims(User user)
        {
            // constructing and returning claims
            return new Claim[]
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim(JwtClaimTypes.Name,user.Login),
                new Claim(JwtClaimTypes.Role, user.RoleId.ToString())
            };
        }
    }
    }


