using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
   public class Admin
    {   [Key()]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name should be minimum 2 characters")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Name should be minimum 4 characters")]
        public string Login { get; set; }
        public string Password { get; set; }
        [ForeignKey("AdminRole")]
        public Role AdminRole { get; set; }
        public int Roles_ID { get; set; }
    }
}
