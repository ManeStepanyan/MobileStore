using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Seller> Sellers { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Admin> Admins { get; set; }
    }
}
