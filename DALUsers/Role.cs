using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
    public class Role
    {   [Key()]
        public int RoleId { get; set; } // change
        [Key()]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [StringLength(40,MinimumLength =5)]
        public string Description { get; set; }
        public ICollection<Seller> Sellers { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Admin> Admins { get; set; }
    }
}
