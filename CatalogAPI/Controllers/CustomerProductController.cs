using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogAPI.Models;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductController : ControllerBase
    {
        private readonly Repo<CustomerProduct> repo;

        public CustomerProductController(Repo<CustomerProduct> repo)
        {
            this.repo = repo;
        }


        // GET: api/CustomerProduct
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.repo.ExecuteOperationAsync("GetAllCustomerProducts");
            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        // GET: api/CustomerProduct/5
        [HttpGet("{id}", Name = "GetProductByCustomerId")]
        public async Task<IActionResult> GetProductByCustomerId(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetProductByCustomerId", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // GET: api/CustomerProduct/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetCustomer", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }


        // POST: api/CustomerProduct
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerProduct customerProduct)
        {
            var res = await this.repo.ExecuteOperationAsync("AddCustomerProduct", new[]
            {
                new KeyValuePair<string, object>("ProductId", customerProduct.ProductId),
                new KeyValuePair<string, object>("CustomerId", customerProduct.CustomerId)
            });
            return new JsonResult(res);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.ExecuteOperationAsync("DeleteCustomerProduct", new[]
            {
                new KeyValuePair<string, object>("id", id)
            });
            return new StatusCodeResult(200);
        }
    }
}

