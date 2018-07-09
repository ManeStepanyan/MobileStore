using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace DALUsers
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
     //   [ForeignKey("SellerRole")]
        public Role SellerRole { get; set; }        
        public int Roles_ID { get; set; }
        public decimal Rating { get; set; }

    }
}
