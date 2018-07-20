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
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly Repo<Order> repo;

        public OrdersController(Repo<Order> repo)
        {
            this.repo = repo;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.repo.ExecuteOperationAsync("GetAllOrders");
            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "GetOrderByOrderId")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetOrderByOrderId", new[] { new KeyValuePair<string, object>("id", id) });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // GET: api/Orders/5
        [HttpGet("{date}", Name = "GetOrderByTimeSpan")]
        public async Task<IActionResult> GetOrderByTimeSpan(DateTime start, DateTime end)
        {
            var res = await this.repo.ExecuteOperationAsync("GetOrderByTimeSpan", new[]
            {
                new KeyValuePair<string, object>("start", start),
                new KeyValuePair<string, object>("end", end)
            });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }


        [HttpGet("{productId}", Name = "GetOrderByProductId")]
        public async Task<IActionResult> GetOrderByProductId(int id)
        {
            var res = await this.repo.ExecuteOperationAsync("GetOrderByProductId", new[]
            {
                new KeyValuePair<string, object>("id", id)
            });
            if (res == null)
            {
                return new StatusCodeResult(404);
            }
            return new JsonResult(res);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            var res = await this.repo.ExecuteOperationAsync("CreateOrder", new[]
            {
                new KeyValuePair<string, object>("ProductId", order.ProductId),
                new KeyValuePair<string, object>("Date", order.Date),
                new KeyValuePair<string, object>("Address", order.Address),
                new KeyValuePair<string, object>("CellPhone", order.CellPhone),
                new KeyValuePair<string, object>("Quantity", order.Quantity),
                new KeyValuePair<string, object>("TotalAmount", order.TotalAmount),
            });
            return new JsonResult(res);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.ExecuteOperationAsync("DeleteOrder", new[] { new KeyValuePair<string, object>("id", id) });
            return new StatusCodeResult(200);
        }
    }
}

