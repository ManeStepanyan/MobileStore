using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public partial class Admin : BaseUser
    {
        public Admin()
        {
            this.Roles_ID = 1;
        }

        public Admin(int? id, string Name, string Password, string Login) : 
            base(id, Name, Password, Login)
        {
            this.Roles_ID = 1;
        }
    }
}
