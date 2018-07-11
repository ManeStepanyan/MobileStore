using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;

namespace UsersAPI.Controllers
{
    public class AdminsController: Controller
    {
        private UsersRepository userRepository;

        public AdminsController()
        {
            this.userRepository = new UsersRepository();
        }

        /*[HttpGet]
        public IEnumerable<Admin> Get()
        {
            //return this.userRepository.GetAdmins();
        }*/

        [HttpGet("{id}", Name = "GetAdmin")]
        public Admin GetAdmin(int id)
        {
            return this.userRepository.GetAdmin(id);
        }

        [HttpPost]
        public void Post([FromBody]Admin admin)
        {
            this.userRepository.AdminSignUp(admin);
        }

        [HttpPut]
        public void Put([FromBody]Admin admin)
        {
            this.userRepository.AdminUpdate(admin);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.userRepository.AdminDelete(id);
        }
    }
}
