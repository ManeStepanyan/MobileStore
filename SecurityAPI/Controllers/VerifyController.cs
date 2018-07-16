using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Verify")]
    public class VerifyController : Controller
    {
        private readonly Repo<UserInformation> repo;
        public VerifyController(Repo<UserInformation> repo)
        {
            this.repo = repo;

        }
        // GET: api/Verify
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Verify/5
        [HttpGet("{id}", Name = "Validate")]
        public async Task<IActionResult> Get(string id)
        {
            await this.repo.ExecuteOperationAsync("VerifyUser", new[] { new KeyValuePair<string, object>("activationCode", id) });
            return new StatusCodeResult(200);
        }
        
        // POST: api/Verify
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Verify/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
