using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    public partial class Seller : BaseUser
    {
        public Seller()
        {
            this.Roles_ID = 2;
        }

        public Seller(int Id, string Name, string Password, string Login, string CellPhone, string Address, decimal? Rating) :
            base(Id, Name, Password, Login)
        {
            this.Roles_ID = 2;
            this.CellPhone = CellPhone;
            this.Address = Address;
            this.Rating = Rating;
        }

        public string CellPhone { get;set; }
        public string Address { get; set; }
        public decimal? Rating { get;  set; }
    }
}
