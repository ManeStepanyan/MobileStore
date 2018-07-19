using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebClient.Controllers
{
   
    public class AccountController : Controller
    {
        // GET: /<controller>/
        [Route("account/login")]
        public  IActionResult Login()
        {
           return View();
        }

        // GET: /<controller>/
        [Route("account/register")]
        public IActionResult Post()
        {
            return View();
        }


    }
}
