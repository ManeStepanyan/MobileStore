using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using CatalogAPI.Models;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/sellerproduct")]
    public class SellerProductController : ControllerBase
    {

        private readonly Repo<SellerProduct> repo;

        public SellerProductController(Repo<SellerProduct> repo)
        {
            this.repo = repo;
        }


        // GET: api/SellerProduct
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.repo.ExecuteOperationAsync("GetAllSellerProducts");
            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        // GET: api/SellerProduct/5
        [HttpGet("{id}", Name = "GetProductBySellerId")]
        public async Task<IActionResult> GetProductBySellerId(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetProductBySellerId", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // GET: api/SellerProduct/5
        [HttpGet("{id}", Name = "GetSellerId")]
        public async Task<IActionResult> GetSellerId(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetSellerId", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }


        // POST: api/SellerProduct
        [HttpPost]
        //  [Authorize(Policy = "Seller")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5002/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            HttpContent content = new ObjectContent(typeof(Product), jsonFormatter, null);
            HttpResponseMessage response = await client.PostAsync("api/products/", content);
            int id = int.Parse(response.ToString());
            return new JsonResult(id);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.ExecuteOperationAsync("DeleteSellerProduct", new[]
            {
                new KeyValuePair<string, object>("id", id)
            });
            return new StatusCodeResult(200);
        }
    }
}

