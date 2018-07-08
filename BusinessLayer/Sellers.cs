using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public partial class Seller : BaseUser
    {
        public Seller()
        {
            this.Roles_ID = 2;
        }

        public Seller(int? id, string Name, string Password, string Login, string CellPhone, string Address, decimal? Rating) :
            base(id, Name, Password, Login)
        {
            this.Roles_ID = 2;
            this.CellPhone = CellPhone;
            this.Address = Address;
            this.Rating = Rating;
        }

        public string CellPhone { get; private set; }
        public string Address { get; private set; }
        public decimal? Rating { get; private set; }
    }
}
