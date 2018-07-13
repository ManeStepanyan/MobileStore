using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cryptography;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/sellers")]
    public class SellersController : Controller
    {
        private Repo<SellerInfo> repo;
        private Repo<SellerPublicInfo> publicRepo;
        private Repo<UserPublicInfo> userRepo;

        public SellersController(Repo<SellerInfo> repo, Repo<SellerPublicInfo> publicRepo, Repo<UserPublicInfo> userRepo)
        {
            this.repo = repo;
            this.publicRepo = publicRepo;
            this.userRepo = userRepo;
        }
        // GET: api/Sellers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.publicRepo.ExecuteOperationAsync("GetAllSellers");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        // GET: api/Sellers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var res = this.publicRepo.ExecuteOperation("GetSeller", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string login)
        {
            var res = this.publicRepo.ExecuteOperation("GetSellerByName", new[] { new KeyValuePair<string, object>("login",login) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // POST: api/Sellers
        [HttpPost]
        public HttpResponseMessage Post([FromBody]SellerInfo seller)
        {
            var response = new HttpResponseMessage();
            if ((int)this.userRepo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", seller.Login) }) != 1)
                return (HttpResponseMessage)this.repo.ExecuteOperation("CreateSeller", new[] { new KeyValuePair<string, object>("name", seller.Name), new KeyValuePair<string, object>("email", seller.Email), new KeyValuePair<string, object>("login", seller.Login), new KeyValuePair<string, object>("password", MyCryptography.Encrypt(seller.Password)) });
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error message");
            }
            return response;
        }

        // PUT: api/Sellers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]SellerInfo seller)
        {
            var userId = int.Parse(
                      ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                      .Where(claim => claim.Type == "user_id").First().Value);

            if (seller.User_Id == userId)
            {
                this.repo.ExecuteOperation("UpdateSeller", new[] { new KeyValuePair<string, object>("id", seller.Id), new KeyValuePair<string, object>("name", seller.Name), new KeyValuePair<string, object>("email", seller.Email), new KeyValuePair<string, object>("login", seller.Login), new KeyValuePair<string, object>("password",MyCryptography.Encrypt( seller.Password)) });
            }
        }
    
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        this.repo.ExecuteOperation("DeleteSeller", new[] { new KeyValuePair<string, object>("id", id) });
    }
        [HttpPut]
        [Route("sellers/id/rate")]
        public void RateSeller(int id, decimal rating)
        {
            this.publicRepo.ExecuteOperation("RateSeller", new[] { new KeyValuePair<string, object>("id", id), new KeyValuePair<string, object>("rating", rating) });

        }
    }
}
