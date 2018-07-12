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

namespace UsersAPI.Controllers
{
    [Route("api/Admin")]
    public class AdminsController: Controller
    {
        
        private Repo<AdminInfo, SpExecuter> repo;
        private Repo<AdminPublicInfo, SpExecuter> publicRepo;
        private Repo<UserPublicInfo, SpExecuter> userRepo;

        public AdminsController()
        {
            this.repo = new Repo<AdminInfo, SpExecuter>("MapInfo\\UserMap.xml");
            this.publicRepo = new Repo<AdminPublicInfo, SpExecuter>("MapInfo\\UserMap.xml");
            this.userRepo = new Repo<UserPublicInfo, SpExecuter>("MapInfo\\UserMap.xml");
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<AdminPublicInfo> Get()
        {
            return (IEnumerable<AdminPublicInfo>) this.publicRepo.ExecuteOperation("GetAllAdmins");
        }

        [HttpGet("{id}", Name = "GetAdmin")]
        [Authorize]
        public AdminPublicInfo GetAdmin(int id)
        {
            var Id = int.Parse(
                   ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                   .Where(claim => claim.Type == "Id").First().Value);
            if (Id == id)
            {
                return (AdminPublicInfo)this.publicRepo.ExecuteOperation("GetAdmin", new[] { new KeyValuePair<string, object>("id", id) });
            }
            else return default(AdminPublicInfo);
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage Post([FromBody]AdminInfo admin)
        {
            var response = new HttpResponseMessage();
            if ((int)this.userRepo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", admin.Login) }) != 1)
                return (HttpResponseMessage)this.repo.ExecuteOperation("CreateAdmin", new[] { new KeyValuePair<string, object>("name", admin.Name), new KeyValuePair<string, object>("email", admin.Email), new KeyValuePair<string, object>("login", admin.Login), new KeyValuePair<string, object>("password", admin.Password) });
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
