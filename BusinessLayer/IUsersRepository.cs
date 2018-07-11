using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUsersRepository
    {
        public async BaseUser FindUserAsync(string login);
        public async BaseUser FindUserAsync(int id);
    }
}
