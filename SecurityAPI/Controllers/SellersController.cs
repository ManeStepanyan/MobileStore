using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;

namespace SecurityAPI.Controllers
{   [Authorize (Roles="Seller")]
    [Route("api/Seller")]
    public class SellersController:Controller
    {
        private UsersRepository userRepository;

        public SellersController()
        {
            this.userRepository = new UsersRepository();
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Seller> Get()
        {
            return this.userRepository.GetAllSellers();
        }

        [HttpGet("{id}", Name = "GetSeller")]
        public Seller GetSeller(int id)
        {
            return this.userRepository.GetSeller(id);
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody]Seller seller)
        {
            this.userRepository.SellerSignUp(seller);
        }

        [HttpPut]
        public void Put([FromBody]Seller seller)
        {
            this.userRepository.SellerUpdate(seller);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.userRepository.SellerDelete(id);
        }
    }
}
