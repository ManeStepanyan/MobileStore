using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DALUsers
{
    public class Seller
    {   [Key()]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(25,MinimumLength =2,ErrorMessage ="Name should be minimum 2 characters")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 9, ErrorMessage = "Cellphone should be minimum 9 characters")]
        public string CellPhone { get; set; }
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Address should be minimum 10 characters")]
        public string Address { get; set; }
        [Key()]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Username should be minimum 4 characters")]
        public string Login { get; set; }
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be minimum 8 characters")]
        public string Password { get; set; }
        [ForeignKey("SellerRole")]
        public Role SellerRole { get; set; }        
        public int Roles_ID { get; set; }
        public decimal Rating { get; set; }

    }
}
