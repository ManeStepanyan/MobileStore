using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public abstract class BaseUser
    {
        public BaseUser()
        {

        }

        public BaseUser(int Id, string Name, string Password, string Login)
        {
            this.Id = this.Id;
            this.Name = Name;
            this.Password = Password;
            this.Login = Login;
        }
        [Required(), Key()]
        public int Id { get;set; } // private set
        public string Name { get; set; }
        public string Password { get;  set; }
        public int Roles_ID { get; set; }
        public string Login { get;  set; }
    }
}
