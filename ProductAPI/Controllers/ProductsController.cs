using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace ProductAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly Repo<Product> repository;
        private readonly Repo<ProductPublicInfo> publicRepository;

        public ProductsController(Repo<Product> repository)
        {
            this.repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.publicRepository.ExecuteOperationAsync("GetProducts");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }


        // GET: api/Products/5
        [HttpGet("{Id}", Name = "Get")]
        public IActionResult Get(int Id)
        {
            var res = this.publicRepository.ExecuteOperation("GetProductById", new[] { new KeyValuePair<string, object>("Id", Id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        [HttpGet("{Name}", Name = "Get")]
        public IActionResult Get(string Name)
        {
            var res = this.publicRepository.ExecuteOperation("GetProductByName", new[] { new KeyValuePair<string, object>("Name", Name) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // POST: api/Products
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Product product)
        {
            return (HttpResponseMessage)this.repository.ExecuteOperation(
                "CreateProduct", new[] {
                    new KeyValuePair<string, object>("Name", product.Name),
                    new KeyValuePair<string, object>("Brand", product.Brand),
                    new KeyValuePair<string, object>("Version", product.Version),
                    new KeyValuePair<string, object>("Price", product.Price),
                    new KeyValuePair<string, object>("RAM", product.RAM),
                    new KeyValuePair<string, object>("Year", product.Year),
                    new KeyValuePair<string, object>("Display", product.Display),
                    new KeyValuePair<string, object>("Battery", product.Battery),
                    new KeyValuePair<string, object>("Camera", product.Camera),
                    new KeyValuePair<string, object>("Image", product.Image),
                    new KeyValuePair<string, object>("Sellers_ID", product.Sellers_ID)
                });
        }


        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int Id, [FromBody]Product product)
        {
            this.repository.ExecuteOperation("UpdateProduct", new[] { new KeyValuePair<string, object>("Id", Id), new KeyValuePair<string, object>("Price", )});
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
