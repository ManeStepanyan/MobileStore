using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class clientik
    {
        public  static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "SuperAdmin", "secret");


            //    var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("sffgggg", "dsdfsss", "UserAPI");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("admin888", "administrator11", "UserAPI");


            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.DeleteAsync("http://localhost:5001/api/sellers/1");
            /*      AdminInfo Admin = new AdminInfo();
                  Admin.Name = "Admin";
                  Admin.Login = "admin888";
                  Admin.Email= "admin789 @gmail.com";
                  Admin.Password = "administrator11";
                  var stringContent = new StringContent( JsonConvert.SerializeObject(Admin));
                  var response = await client.PostAsync("http://localhost:5001/api/admins", stringContent); */




            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));

            }
            Console.Read();
        }
    }
}
