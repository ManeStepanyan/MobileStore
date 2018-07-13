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
  //  [Authorize]
    public class AdminsController: Controller
    {
       // private MapInfo mapInfo;
       // private ISpExecuter spExecuter;
        private Repo<AdminInfo> repo;
        private Repo<AdminPublicInfo> publicRepo;
        private Repo<UserPublicInfo> userRepo;

        public AdminsController(Repo<AdminInfo> repo, Repo<AdminPublicInfo> publicRepo,Repo<UserPublicInfo> userRepo)
        {
            this.repo = repo;
            this.publicRepo = publicRepo;
            this.userRepo = userRepo;
        }

        [HttpGet]
      //  [Authorize]
    /*    public IEnumerable<AdminPublicInfo> Get()
        {
            return (IEnumerable<AdminPublicInfo>) this.publicRepo.ExecuteOperation("GetAllAdmins");
        } */
        public async Task<IActionResult> Get()
        {
            var result = await this.publicRepo.ExecuteOperationAsync("GetAllAdmins");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        [HttpGet("{id}", Name = "GetAdmin")]
        [Authorize]
        public IActionResult GetAdmin(int id)
        {
           var res= this.publicRepo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            } return new JsonResult(res);
            /*   var userId = int.Parse(
                      ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                      .Where(claim => claim.Type == "user_id").First().Value);
               if (((AdminInfo)this.repo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) })).User_Id == userId)
               {
                   return (AdminPublicInfo)this.publicRepo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) });
               }

               else return default(AdminPublicInfo); */
        }
        [HttpGet("{id}", Name = "GetAdmin")]
        [Authorize]
        public IActionResult GetAdmin(string login)
        {
            var res = this.publicRepo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("login", login) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        [HttpPost]
       // [Authorize]
        public HttpResponseMessage Post([FromBody]AdminInfo admin)
        {
            var response = new HttpResponseMessage();
            if ((int)this.userRepo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", admin.Login) }) != 1)
                return (HttpResponseMessage)this.repo.ExecuteOperation("CreateAdmin", new[] { new KeyValuePair<string, object>("name", admin.Name), new KeyValuePair<string, object>("email", admin.Email), new KeyValuePair<string, object>("login", admin.Login), new KeyValuePair<string, object>("password",MyCryptography.Encrypt(admin.Password)) });
            else
            {               
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error message");
            }
            return response;
        }

        [HttpPut]
        [Authorize]
        public void Put([FromBody]AdminInfo admin)
        {
            var userId = int.Parse(
                   ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                   .Where(claim => claim.Type == "user_id").First().Value);

            if (admin.User_Id == userId)
            {
                this.repo.ExecuteOperation("UpdateAdmin", new[] { new KeyValuePair<string, object>("id", admin.Id), new KeyValuePair<string, object>("name", admin.Name), new KeyValuePair<string, object>("email", admin.Email), new KeyValuePair<string, object>("login", admin.Login), new KeyValuePair<string, object>("password", admin.Password) });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            this.repo.ExecuteOperation("DeleteAdmin", new[] { new KeyValuePair<string, object>("id", id) });
        }
    }
}
