using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [Route("api/Customer")]
    public class CustomersController : Controller
    {
        private UsersRepository userRepository;

        public CustomersController()
        {
            this.userRepository = new UsersRepository();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
/*
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return this.userRepository.GetAllCustomer();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public Customer GetCustomer(int id)
        {
            return this.userRepository.GetCustomer(id);
        }

        [HttpPost]
        public void Post([FromBody]Customer customer)
        {
            this.userRepository.CustomerSignUp(customer);
        }

        [HttpPut]
        public void Put([FromBody]Customer customer)
        {
            this.userRepository.CustomerUpdate(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.userRepository.CustomerDelete(id);
        }*/
    }
}
