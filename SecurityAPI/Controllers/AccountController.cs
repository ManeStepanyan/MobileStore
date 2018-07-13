using System.Collections.Generic;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private Repo<UserInformation> repo;
        public AccountController(Repo<UserInformation> repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Post([FromBody]UserInformation user)
        {
            // adding user
            if ((int)this.repo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", user.Login) }) != 1)
               this.repo.ExecuteOperation("CreateUser", user);         

            // if user exists return 'Conflict' code
          else
                return new StatusCodeResult(409);


            // returning 200
            return Ok();
        }
        
    }
}