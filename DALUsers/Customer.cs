using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
   
    public class Customer
    {  [Key()]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name should be minimum 2 characters")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Surname should be minimum 4 characters")]
        public string Surname { get; set; }
        [Key()]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "Email should be minimum 10 characters")]
        public string Email { get; set; }
        [Key()]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Username should be minimum 4 characters")]
        public string Login { get; set; }
        public string Password { get; set; }
        [ForeignKey("CustomerRole")]
        public Role CustomerRole { get; set; }
        public int Roles_ID { get; set; }
        public bool Status { get; set; }
    }
}
