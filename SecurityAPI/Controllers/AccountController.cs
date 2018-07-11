using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("/SignUp")] 
        public IActionResult SignUp()
        {
            return View();
        }
    }
}