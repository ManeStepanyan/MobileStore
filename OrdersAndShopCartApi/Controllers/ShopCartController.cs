using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersAndShopCartAPI.Models;

namespace OrdersAndShopCartAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/shopcart")]
    public class ShopCartController : ControllerBase
    {

        private readonly Repo<ShopCart> repo;

        public ShopCartController(Repo<ShopCart> repo)
        {
            this.repo = repo;
        }

        // GET: api/ShopCart
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.repo.ExecuteOperationAsync("GetAllShopCarts");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        [HttpGet("{id}", Name = "GetShopCartById")]
        public async Task<IActionResult> GetShopCartByOrderId(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetShopCartById", new[]
            {
                new KeyValuePair<string, object>("id", id)
            });

            if (res == null)
            {
                return new StatusCodeResult(404);
            }

            return new JsonResult(res);
        }

        // POST: api/shopcart
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopCart shopCart)
        {
            var res = await this.repo.ExecuteOperationAsync("AddToShopCart", new[]
            {
                new KeyValuePair<string, object>("CostumerId", shopCart.CostumerId),
                new KeyValuePair<string, object>("ProductId", shopCart.ProductId),
                new KeyValuePair<string, object>("Quantity", shopCart.Quantity),
            });
            return new JsonResult(res);
        }

        // PUT: api/ShopCart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, int quantity)
        {
            await this.repo.ExecuteOperationAsync("UpdateShopCart", new[]
            {
                new KeyValuePair<string, object>("id", id),
                new KeyValuePair<string, object>("quantity", quantity)
            });

            return new JsonResult(await this.repo.ExecuteOperationAsync("GetShopCartById", new[]
                                  { new KeyValuePair<string, object>("id", id) }));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.ExecuteOperationAsync("DeleteFromShopCart", new[] { new KeyValuePair<string, object>("id", id) });
            return new StatusCodeResult(200);
        }
    }
}
