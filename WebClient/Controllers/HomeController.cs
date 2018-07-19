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

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("home/seller")]
        public async Task<IActionResult> SellerAsync()
        {
            // ... Target page.
            Uri siteUri = new Uri("http://localhost:5001/api/sellers");
            List<SellerModel> sellers = new List<SellerModel>();

            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(siteUri))
                {
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        string result = await content.ReadAsStringAsync();
                        sellers = JsonConvert.DeserializeObject<List<SellerModel>>(result);
                        /*foreach(var seller in obj)
                        {
                            sellers.Add(obj);
                        }*/
                       
                        if (result != null &&
                            result.Length >= 50)
                        {
                            Console.WriteLine(result.Substring(0, 50) + "...");
                        }
                    }
                }
            }
            return View(sellers);
        }

        [Route("home/seller/{id}")]
        public async Task<IActionResult> Seller(int id)
        {
            // ... Target page.
            Uri siteUri = new Uri("http://localhost:5001/api/"+id);
            List<SellerModel> sellers = new List<SellerModel>();

            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(siteUri))
                {
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        string result = await content.ReadAsStringAsync();
                        sellers = JsonConvert.DeserializeObject<List<SellerModel>>(result);
                        /*foreach(var seller in obj)
                        {
                            sellers.Add(obj);
                        }*/

                        if (result != null &&
                            result.Length >= 50)
                        {
                            Console.WriteLine(result.Substring(0, 50) + "...");
                        }
                    }
                }
            }
            return View(sellers);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
