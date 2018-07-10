using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUsersRepository
    {
        BaseUser FindUserAsync(string login);
        BaseUser FindUserAsync(int id);
    }
}
