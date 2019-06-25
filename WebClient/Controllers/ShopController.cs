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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebClient.Controllers
{
    public class ShopController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

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

        //[Route("home/seller/{id}")]
        /* public async Task<IActionResult> Seller(int id)
         {
             // ... Target page.
             Uri idSiteUri = new Uri("http://localhost:5001/api/" + id);
             Uri prodSiteUri = new Uri("http://localhost:5002/api/");
             List<KeyValuePair<string, int>> product_ids = new List<KeyValuePair<string, int>>();

             // ... Use HttpClient.
             using (HttpClient client = new HttpClient())
             {
                 using (HttpResponseMessage response = await client.GetAsync(idSiteUri))
                 {
                     using (HttpContent content = response.Content)
                     {
                         // ... Read the string.
                         string result = await content.ReadAsStringAsync();
                         product_ids = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(result);
                         using (HttpResponseMessage res = await client.GetAsync(prodSiteUri, product_ids))
                         {

                         }
                         foreach(var seller in obj)
                         {
                             sellers.Add(obj);
                         }

                         if (result != null &&
                         result.Length >= 50)
                         {
                             Console.WriteLine(result.Substring(0, 50) + "...");
                         }
                     }
                 }
             }
             return View(sellers);
         }*/
    }   
}
