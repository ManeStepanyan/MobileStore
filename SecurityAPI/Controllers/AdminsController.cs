using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Repository;
using UsersAPI.Models;
using DatabaseAccess.SpExecuters;
using System.Linq;
using System.Net.Http;
using System.Net;
using DatabaseAccessor.Repository;
using Cryptography;
namespace UsersAPI.Controllers
{
    [Route("api/admins")]
  //  [Authorize]
    public class AdminsController: Controller
    {
        private MapInfo mapInfo;
        private ISpExecuter spExecuter;
        private Repo<AdminInfo> repo;
        private Repo<AdminPublicInfo> publicRepo;
        private Repo<UserPublicInfo> userRepo;

        public AdminsController()
        {
            this.repo = new Repo<AdminInfo>(mapInfo,spExecuter);
            this.publicRepo = new Repo<AdminPublicInfo>(mapInfo,spExecuter);
            this.userRepo = new Repo<UserPublicInfo>(mapInfo,spExecuter);
        }

        [HttpGet]
      //  [Authorize]
        public IEnumerable<AdminPublicInfo> Get()
        {
            return (IEnumerable<AdminPublicInfo>) this.publicRepo.ExecuteOperation("GetAllAdmins");
        }

        [HttpGet("{id}", Name = "GetAdmin")]
        [Authorize]
        public AdminPublicInfo GetAdmin(int id)
        {
            var userId = int.Parse(
                   ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                   .Where(claim => claim.Type == "user_id").First().Value);
            if (((AdminInfo)this.repo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) })).User_Id == userId)
            {
                return (AdminPublicInfo)this.publicRepo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) });
            }

            else return default(AdminPublicInfo);
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
