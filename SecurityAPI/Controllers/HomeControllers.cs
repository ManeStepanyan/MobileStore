using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace UsersAPI.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/home")]
    public class HomeControllers: Controller
    {
        [AllowAnonymous]
        [Route("")]      // Combines to define the route template "Home"
        [Route("Index")] // Combines to define the route template "Home/Index"
        [Route("/")]     // Does not combine, defines the route template ""
        public IActionResult Index()
        {
                return View();
        }
    }
}
