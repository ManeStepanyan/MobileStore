using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
   public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role AdminRole { get; set; }
        public int Roles_ID { get; set; }
    }
}
