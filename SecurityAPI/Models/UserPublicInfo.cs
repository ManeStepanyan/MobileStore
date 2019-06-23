using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersAPI.Models
{
    public class UserPublicInfo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

    }
}
