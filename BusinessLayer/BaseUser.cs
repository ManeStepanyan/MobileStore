using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public abstract class BaseUser
    {
        public BaseUser()
        {

        }

        public BaseUser(int? id, string Name, string Password, string Login)
        {
            this.Id = Id;
            this.Name = Name;
            this.Password = Password;
            this.Login = Login;
        }

        public int? Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public int Roles_ID { get; protected set; }
        public string Login { get; private set; }
    }
}
