using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Repository;
using UsersAPI.Models;
using System.Linq;
using System.Net.Http;
using System.Net;
using Cryptography;
using System.Threading.Tasks;

namespace UsersAPI.Controllers
{
    [Route("api/admins")]
    [Authorize(Policy = "Admin")]
    public class AdminsController : Controller
    {
        // private MapInfo mapInfo;
        // private ISpExecuter spExecuter;
        private Repo<AdminInfo> repo;
        private Repo<AdminPublicInfo> publicRepo;
        private Repo<UserPublicInfo> userRepo;

        public AdminsController(Repo<AdminInfo> repo, Repo<AdminPublicInfo> publicRepo, Repo<UserPublicInfo> userRepo)
        {
            this.repo = repo;
            this.publicRepo = publicRepo;
            this.userRepo = userRepo;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        /*    public IEnumerable<AdminPublicInfo> Get()
            {
                return (IEnumerable<AdminPublicInfo>) this.publicRepo.ExecuteOperation("GetAllAdmins");
            } */
        public async Task<IActionResult> Get()
        {
            var userId = int.Parse(
                ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
            if (userId == 8) //our super admin 
            {
                var result = await this.publicRepo.ExecuteOperationAsync("GetAllAdmins");

                if (result == null)
                    return new StatusCodeResult(204);

                return new JsonResult(result);
            }
            return new StatusCodeResult(204);
        }

        [HttpGet("{id}", Name = "GetAdminById")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAdminById(int id)
        {
            var userId = int.Parse(
                    ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                    .Where(claim => claim.Type == "user_id").First().Value);
            if (((AdminInfo)this.repo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) })).User_Id == userId)
            {
                var res = this.publicRepo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) });
                if (res == null)
                {
                    return new StatusCodeResult(404);
                }
                return new JsonResult(res);
            }
            return new StatusCodeResult(404);
        }
        [HttpGet("login/{login}", Name = "GetAdminByLogin")]

        [Authorize(Policy = "Admin")]
        public IActionResult GetAdminByLogin(string login)
        {
            var userId = int.Parse(
                ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
            if (((UserPublicInfo)(this.userRepo.ExecuteOperation("GetUserByName", new[] { new KeyValuePair<string, object>("login", login) }))).Id == userId)
            {
                var res = (AdminPublicInfo)this.publicRepo.ExecuteOperation("GetAdminByName", new[] { new KeyValuePair<string, object>("login", login) });

                if (res == null)
                {
                    return new StatusCodeResult(404);
                }
                return new JsonResult(res);
            }
            return new StatusCodeResult(404);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult Post([FromBody]AdminInfo admin)
        {
            var userId = int.Parse(
               ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
               .Where(claim => claim.Type == "user_id").First().Value);
            if (userId == 8) //our super admin 
            {
                if ((int)this.userRepo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", admin.Login) }) != 1)
                {
                    var res = this.repo.ExecuteOperation("CreateAdmin", new[] { new KeyValuePair<string, object>("name", admin.Name), new KeyValuePair<string, object>("email", admin.Email), new KeyValuePair<string, object>("login", admin.Login), new KeyValuePair<string, object>("password", MyCryptography.Encrypt(admin.Password)) });
                    return new JsonResult(res);
                }
                else
                {
                    return new StatusCodeResult(404);
                }
            }
            return new StatusCodeResult(404);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]AdminInfo admin)
        {
            var userId = int.Parse(
                        ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                        .Where(claim => claim.Type == "user_id").First().Value);
            if (((AdminInfo)this.repo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", admin.Id) })).User_Id == userId)

            {
                this.repo.ExecuteOperation("UpdateAdmin", new[] { new KeyValuePair<string, object>("id", admin.Id), new KeyValuePair<string, object>("name", admin.Name), new KeyValuePair<string, object>("email", admin.Email), new KeyValuePair<string, object>("login", admin.Login), new KeyValuePair<string, object>("password", admin.Password) });
                return this.GetAdminById(admin.Id); }
            return new StatusCodeResult(404);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(
                          ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                          .Where(claim => claim.Type == "user_id").First().Value);
            if (((AdminInfo)this.repo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) })).User_Id == userId || userId == 8)
            {
                this.repo.ExecuteOperation("DeleteAdmin", new[] { new KeyValuePair<string, object>("id", id) });
            } return new StatusCodeResult(404);
        }
    }
}
