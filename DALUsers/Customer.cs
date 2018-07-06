using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
   public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role CustomerRole { get; set; }
        public int Roles_ID { get; set; }
        public bool Status { get; set; }
    }
}
