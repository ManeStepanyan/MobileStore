using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public partial class Customer : BaseUser
    {
        public Customer()
        {
            this.Roles_ID = 3;
        }

        public Customer(int? id, string Name, string Password, string Login,
            string Surname, string Email) :
            base(id, Name, Password, Login)
        {
            this.Roles_ID = 3;
            this.Surname = Surname;
            this.Email = Email;
            this.Status = false;
        }

        public string Surname { get; private set; }
        public string Email { get; private set; }
        public bool Status { get; private set; }
    }
}
