using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class SellerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public decimal Rating { get; set; }
    }
}
