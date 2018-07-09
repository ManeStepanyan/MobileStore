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

        public Customer(int? Id, string Name, string Password, string Login,
            string Surname, string Email) :
            base(Id, Name, Password, Login)
        {
            this.Roles_ID = 3;
            this.Surname = Surname;
            this.Email = Email;
            this.Status = false;
        }

        public string Surname { get;  set; }
        public string Email { get; set; }
        public bool Status { get;  set; }
    }
}
