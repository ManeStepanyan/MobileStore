using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cryptography;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private Repo<CustomerInfo> repo;
        private Repo<CustomerPublicInfo> publicRepo;
        private Repo<UserPublicInfo> userRepo;

        public CustomersController(Repo<CustomerInfo> repo, Repo<CustomerPublicInfo> publicRepo, Repo<UserPublicInfo> userRepo)
        {
            this.repo = repo;
            this.publicRepo = publicRepo;
            this.userRepo = userRepo;
        }
       
        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.publicRepo.ExecuteOperationAsync("GetAllCustomers");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var res = this.publicRepo.ExecuteOperation("GetCustomer", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string login)
        {
            var res = this.publicRepo.ExecuteOperation("GetCustomerByName", new[] { new KeyValuePair<string, object>("login", login) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }


        // POST: api/Customers
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CustomerInfo customer)
        {
            var response = new HttpResponseMessage();
            if ((int)this.userRepo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", customer.Login) }) != 1)
                return (HttpResponseMessage)this.repo.ExecuteOperation("CreateCustomer", new[] { new KeyValuePair<string, object>("name", customer.Name), new KeyValuePair<string, object>("email", customer.Email), new KeyValuePair<string, object>("login", customer.Login), new KeyValuePair<string, object>("password", MyCryptography.Encrypt(customer.Password)) });
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error message");
            }
            return response;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]CustomerInfo customer)
        {
            var userId = int.Parse(
                       ((System.Security.Claims.ClaimsIdentity)this.User.Identity).Claims
                       .Where(claim => claim.Type == "user_id").First().Value);

            if (customer.User_Id == userId)
            {
                this.repo.ExecuteOperation("UpdateCustomer", new[] { new KeyValuePair<string, object>("id", customer.Id), new KeyValuePair<string, object>("name", customer.Name), new KeyValuePair<string, object>("email", customer.Email), new KeyValuePair<string, object>("login", customer.Login), new KeyValuePair<string, object>("password",MyCryptography.Encrypt(customer.Password)) });
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.repo.ExecuteOperation("DeleteCustomer", new[] { new KeyValuePair<string, object>("id", id) });
        }
        [HttpPost]
        [Route("api/customer/changestatus")]
        public void ChangeStatus(int id, bool isTrue)
        {
            this.repo.ExecuteOperation("ChangeStatus", new[]{new KeyValuePair<string, object>("id", id), new KeyValuePair<string, object>("isTrue", isTrue) });
        }
    }
}
