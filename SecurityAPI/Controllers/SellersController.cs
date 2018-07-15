using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Repository;
using UsersAPI.Models;
using System.Linq;
using Cryptography;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace UsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/sellers")]
    public class SellersController : Controller
    {
        private readonly Repo<SellerInfo> repo;
        private readonly Repo<SellerPublicInfo> publicRepo;
        private readonly Repo<UserPublicInfo> userRepo;

        public SellersController(Repo<SellerInfo> repo, Repo<SellerPublicInfo> publicRepo, Repo<UserPublicInfo> userRepo)
        {
            this.repo = repo;
            this.publicRepo = publicRepo;
            this.userRepo = userRepo;
        }
        // GET: api/Sellers
        [HttpGet(Name = "GetSellers")]
        public async Task<IActionResult> GetSellers()
        {
            var result = await this.publicRepo.ExecuteOperationAsync("GetAllSellers");
            if (result == null)
                return new StatusCodeResult(204);
            return new JsonResult(result);
        }

        // GET: api/Sellers/5
        [HttpGet("{id}", Name = "GetSellerById")]
        public async Task<IActionResult> GetById(int id)
        {

            var seller = await this.publicRepo.ExecuteOperationAsync("GetSeller", new[] { new KeyValuePair<string, object>("id", id) });
            if (seller == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(seller);

        }
        [HttpGet("login/{login}", Name = "GetSellerByLogin")]
        public async Task<IActionResult> GetByName(string login)
        {
            var res = await this.publicRepo.ExecuteOperationAsync("GetSellerByName", new[] { new KeyValuePair<string, object>("login", login) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // POST: api/Sellers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SellerInfo seller)
        {
            if ((int)this.userRepo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", seller.Login) }) == 1)
            {
                throw new System.Exception("Username already exists");
            }
            var res = await this.repo.ExecuteOperationAsync("CreateSeller", new[] { new KeyValuePair<string, object>("name", seller.Name), new KeyValuePair<string, object>("email", seller.Email), new KeyValuePair<string, object>("cellphone", seller.CellPhone), new KeyValuePair<string, object>("address", seller.Address), new KeyValuePair<string, object>("login", seller.Login), new KeyValuePair<string, object>("password", MyCryptography.Encrypt(seller.Password)) });
            return new JsonResult(res);
        }


        // PUT: api/Sellers/5
        [HttpPut("{id}")]
        [Authorize(Policy = "Seller")]
        public async Task<IActionResult> Put(int id, [FromBody]SellerInfo seller)
        {
            var userId = int.Parse(
                           ((ClaimsIdentity)this.User.Identity).Claims
                           .Where(claim => claim.Type == "user_id").First().Value);
            if (userId == ((SellerInfo)(await this.repo.ExecuteOperationAsync("GetSeller", new[] { new KeyValuePair<string, object>("id", id) }))).UserId)
            {
                await this.repo.ExecuteOperationAsync("UpdateSeller", new[] { new KeyValuePair<string, object>("id", id), new KeyValuePair<string, object>("name", seller.Name = seller.Name ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("cellphone", seller.CellPhone = seller.CellPhone ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("address", seller.Address = seller.Address ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("email", seller.Email = seller.Email ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("login", seller.Login = seller.Login ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("password", seller.Password= MyCryptography.Encrypt(seller.Password)?? DBNull.Value.ToString()) });
                 return await this.GetById(id);
            }
                return new StatusCodeResult(404);
            
        } 


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
      //  [Authorize(Policy = "Admin")]
    //    [Authorize(Policy = "Seller")]
        [Authorize( "Admin, Seller")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = int.Parse(((ClaimsIdentity)this.User.Identity).Claims.Where(claim => claim.Type == "role").First().Value);
            if (role == 2)
            {
                var userId = int.Parse(
                              ((ClaimsIdentity)this.User.Identity).Claims
                              .Where(claim => claim.Type == "user_id").First().Value);
                if (((SellerInfo) await this.repo.ExecuteOperationAsync("GetSeller", new[] { new KeyValuePair<string, object>("id", id) })).UserId != userId)
                {
                    return new StatusCodeResult(404);
                }
            }
         await this.repo.ExecuteOperationAsync("DeleteSeller", new[] { new KeyValuePair<string, object>("id", id) });
            return new StatusCodeResult(200);
        }
        [HttpPut]
        [Route("sellers/id/rate")]
        public void RateSeller(int id, decimal? rating)
        {
            this.publicRepo.ExecuteOperation("RateSeller", new[] { new KeyValuePair<string, object>("id", id), new KeyValuePair<string, object>("rating", rating) });

        }
    }
}
