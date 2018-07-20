using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly Repo<Product> repository;

        public ProductsController(Repo<Product> repository)
        {
            this.repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.repository.ExecuteOperationAsync("GetAllProducts");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }


        // GET: api/Products/5
        [HttpGet("{Id}", Name = "GetProductById")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await this.repository.ExecuteOperationAsync("GetProduct", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        [HttpGet("{Name}", Name = "GetProductByName")]
        public async Task<IActionResult> Get(string name)
        {
            var res = await this.repository.ExecuteOperationAsync("GetProductByName", new[] { new KeyValuePair<string, object>("name", name) });

            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
           var res = await this.repository.ExecuteOperationAsync("CreateProduct", new[] 
           {
              new KeyValuePair<string, object>("name", product.Name),
              new KeyValuePair<string, object>("brand", product.Brand),
              new KeyValuePair<string, object>("version", product.Version),
              new KeyValuePair<string, object>("price", product.Price),
              new KeyValuePair<string, object>("ram", product.RAM),
              new KeyValuePair<string, object>("year", product.Year),
              new KeyValuePair<string, object>("display", product.Display),
              new KeyValuePair<string, object>("battery", product.Battery),
              new KeyValuePair<string, object>("camera", product.Camera),
              new KeyValuePair<string, object>("image", product.Image)
           });

            return new JsonResult(res);
        } 


        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int Id, double Price, string Image)
        {
            await this.repository.ExecuteOperationAsync("UpdateProduct", new[]
            {
                new KeyValuePair<string, object>("id", Id),
                new KeyValuePair<string, object>("price", Price),
                new KeyValuePair<string, object>("image", Image)
            });

            return new JsonResult(await this.repository.ExecuteOperationAsync("GetProduct", new[]
                                  { new KeyValuePair<string, object>("Id", Id) }));
        }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repository.ExecuteOperationAsync("DeleteProduct", new[] {
                new KeyValuePair<string, object>("id", id),
            });
            return new StatusCodeResult(200);
        } 
    } 
}


