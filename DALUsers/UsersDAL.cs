using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
    public class UsersDAL
    {
        private readonly string connectionString;
        public UsersDAL()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["NameOfMyStringConnection"].ConnectionString;
        }
    }
    
}
