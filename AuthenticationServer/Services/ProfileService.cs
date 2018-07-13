using System;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationServer.Validators;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using AuthenticationServer.UsersRepository;

namespace AuthenticationServer.Services
{  /// <summary>
   /// Profile serviice
   /// </summary>
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// User repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates new instance of 
        /// <see cref="ProfileService"/> with the given user repository.
        /// </summary>
        /// <param name="userRepository">User repository</param>
        public ProfileService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Gets profile data.
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>profile data getting task</returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                context.IssuedClaims.AddRange(context.Subject.Claims);
                // depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    // get user from db (in my case this is by email)
                    var user = await this._userRepository.FindAsync(context.Subject.Identity.Name);

                    // checking user
                    if (user != null)
                    {
                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                        // set issued claims to return
                        context.IssuedClaims.AddRange(claims);
                    }
                }
                else
                {
                    // get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    // where and subject was set to my user id.
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                    {
                        // get user from db (find user by user id)
                        var user = await this._userRepository.FindAsync(int.Parse(userId.Value));

                        // issue the claims for the user
                        if (user != null)
                        {
                            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                            context.IssuedClaims.AddRange(claims);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }


        /// <summary>
        /// Checks if user is active.
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>activity checking task</returns>
          public async Task IsActiveAsync(IsActiveContext context)
           {
               try
               {
                   // get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                   var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                   if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                   {
                       var user = await this._userRepository.FindAsync(int.Parse(userId.Value));

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
                   // handle error logging
               }
           } 
    }
}
