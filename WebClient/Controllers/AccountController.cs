using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using IdentityModel.Client;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebClient.Controllers
{
   
    public class AccountController : Controller
    {
        // GET: /<controller>/
        [Route("account/login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel user)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(user.Username,user.Password,"UserAPI");

            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }
            return new JsonResult(tokenResponse.Json);
        }

        // GET: /<controller>/
        /*[Route("account/register")]
        public async Task<IActionResult> t RegisternAsync()
        {
            return View();
        }
        */

    }
}
