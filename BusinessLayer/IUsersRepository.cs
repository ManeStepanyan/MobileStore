using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUsersRepository
    {
        public BaseUser FindUser(string login);
        public BaseUser FindUser(int id);
    }
}
