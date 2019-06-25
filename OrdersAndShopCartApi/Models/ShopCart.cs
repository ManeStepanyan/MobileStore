using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAndShopCartAPI.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public int CostumerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
