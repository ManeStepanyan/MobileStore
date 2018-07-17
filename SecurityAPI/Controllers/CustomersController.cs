using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccess.Repository;
using UsersAPI.Models;
using System.Linq;
using System.Security.Claims;
using Cryptography;
using System.Threading.Tasks;
using System;

namespace UsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly Repo<CustomerInfo> repo;
        private readonly Repo<CustomerPublicInfo> publicRepo;
        private readonly Repo<UserPublicInfo> userRepo;

        public CustomersController(Repo<CustomerInfo> repo, Repo<CustomerPublicInfo> publicRepo, Repo<UserPublicInfo> userRepo)
        {
            this.repo = repo;
            this.publicRepo = publicRepo;
            this.userRepo = userRepo;
        }
        // GET: api/Customers
        [HttpGet (Name= "GetCustomers")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await this.publicRepo.ExecuteOperationAsync("GetAllCustomers");
            if (result == null)
                return new StatusCodeResult(204);
            return new JsonResult(result);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "GetByN")]
        [Authorize(Policy = "Admin, Customer")]
        public async Task<IActionResult> Get(int id)
        {
            CustomerInfo customer = new CustomerInfo();
            var role = int.Parse((((ClaimsIdentity)this.User.Identity).Claims.Where(claim => claim.Type == "role").First().Value));
            customer = (CustomerInfo)(await this.publicRepo.ExecuteOperationAsync("GetCustomer", new[] { new KeyValuePair<string, object>("id", id) }));
            if (role == 3)
            {
                var userId = int.Parse(
                        ((ClaimsIdentity)this.User.Identity).Claims
                        .Where(claim => claim.Type == "user_id").First().Value);
                if (userId != customer.UserId || customer == null)
                {
                    return new StatusCodeResult(404);
                }
            }
            return new JsonResult(customer);

        }
        [HttpGet("login/{login}", Name = "GetByLogin")]
        [Authorize(Policy = "Admin, Customer")]
        public async Task<IActionResult> Get(string login)
        {
            CustomerPublicInfo customer = new CustomerPublicInfo();
            var role = int.Parse((((ClaimsIdentity)this.User.Identity).Claims.Where(claim => claim.Type == "role").First().Value));
            customer = (CustomerPublicInfo)(await this.publicRepo.ExecuteOperationAsync("GetCustomerByName", new[] { new KeyValuePair<string, object>("login",login) }));
            if (role == 3)
            {
                var userId = int.Parse(
                        ((ClaimsIdentity)this.User.Identity).Claims
                        .Where(claim => claim.Type == "user_id").First().Value);
                if (userId != ((CustomerInfo)(await this.repo.ExecuteOperationAsync("GetCustomerByName", new[] { new KeyValuePair<string, object>("login", login) }))).UserId || customer == null)
                {
                    return new StatusCodeResult(404);
                }
            }
            return new JsonResult(customer);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerInfo customer)
        {

            if ((int)await this.userRepo.ExecuteOperationAsync("ExistsLogin", new[] { new KeyValuePair<string, object>("login", customer.Login) }) == 1)
            {
                throw new System.Exception("Username already exists");
            }
            var res = await this.repo.ExecuteOperationAsync("CreateCustomer", new[] { new KeyValuePair<string, object>("name", customer.Name), new KeyValuePair<string, object>("surname", customer.Surname), new KeyValuePair<string, object>("email", customer.Email), new KeyValuePair<string, object>("login", customer.Login), new KeyValuePair<string, object>("password", MyCryptography.Encrypt(customer.Password)) });
            return new JsonResult(res);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        [Authorize("Customer")]
        public async Task <IActionResult> Put(int id, [FromBody]CustomerInfo customer)
        {
            var userId = int.Parse(
                      ((ClaimsIdentity)this.User.Identity).Claims
                      .Where(claim => claim.Type == "user_id").First().Value);
       
            if (((CustomerInfo)(await this.repo.ExecuteOperationAsync("GetCustomer", new[] { new KeyValuePair<string, object>("id", id) }))).UserId == userId)
            {
              await this.repo.ExecuteOperationAsync("UpdateCustomer", new[] { new KeyValuePair<string, object>("id", id), new KeyValuePair<string, object>("name", customer.Name = customer.Name?? DBNull.Value.ToString()), new KeyValuePair<string, object>("surname", customer.Surname = customer.Surname ?? DBNull.Value.ToString()),new KeyValuePair<string, object>("email", customer.Email=customer.Email ?? DBNull.Value.ToString())s, new KeyValuePair<string, object>("password", customer.Password= MyCryptography.Encrypt(customer.Password) ?? DBNull.Value.ToString()) });
                return new JsonResult(await this.Get(id));
            }
            return new StatusCodeResult(404);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin, Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = int.Parse(((ClaimsIdentity)this.User.Identity).Claims.Where(claim => claim.Type == "role").First().Value);
            if (role == 3)
            {
                var userId = int.Parse(((ClaimsIdentity)this.User.Identity).Claims
                              .Where(claim => claim.Type == "user_id").First().Value);
                if (((CustomerInfo)(await this.repo.ExecuteOperationAsync("GetCustomer", new[] { new KeyValuePair<string, object>("id", id) }))).UserId != userId)
                {
                    return new StatusCodeResult(404);
                }
            }
           await this.repo.ExecuteOperationAsync("DeleteCustomer", new[] { new KeyValuePair<string, object>("id", id) });
            return new StatusCodeResult(200);
        }

    }
}
