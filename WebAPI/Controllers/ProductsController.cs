using DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [System.Web.Http.Route("api/products")]
    public class ProductsController : ApiController
    {
        private readonly Repo<Product> repository;
        private readonly Repo<ProductPublicInfo> publicRepository;

        public ProductsController(Repo<Product> repository)
        {
            this.repository = repository;
        }

        // GET: api/Products
       /* [System.Web.Http.HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.publicRepository.ExecuteOperationAsync("GetProducts");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }*/

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult GetAdmin(int Id)
        {
            var res = this.publicRepository.ExecuteOperation("GetProductById", new[] { new KeyValuePair<string, object>("Id", Id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }


        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
