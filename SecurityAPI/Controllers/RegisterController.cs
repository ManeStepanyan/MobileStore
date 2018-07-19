using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cryptography;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Register")]
    public class RegisterController : Controller
    {   private readonly Repo<UserInformation> repo;
        public RegisterController(Repo<UserInformation> repo)
        {
            this.repo = repo;

        }
        // GET: api/Register
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
     /*   [HttpGet("{id}", Name = "GetById")]
        public string Get(int id)
        {
            return "value";
        } */

        // POST: api/Register
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInformation user)
        {
            user.ActivationCode = Guid.NewGuid().ToString();
            if ((int)this.repo.ExecuteOperation("ExistsLogin", new[] { new KeyValuePair<string, object>("login", user.Login) }) == 1)
            {
                return new JsonResult("Such a username exists");
            }
             await this.repo.ExecuteOperationAsync("CreateUser", new[] { new KeyValuePair<string, object>("name", user.Name), new KeyValuePair<string, object>("surname", user.Surname ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("email", (this.IsValidEmail( user.Email))?user.Email:throw new Exception("Invalid Email")), new KeyValuePair<string, object>("address",user.Address ?? DBNull.Value.ToString()), new KeyValuePair<string, object>("cellphone", user.CellPhone?? DBNull.Value.ToString()), new KeyValuePair<string, object>("login", user.Login), new KeyValuePair<string, object>("password", MyCryptography.Encrypt(user.Password)), new KeyValuePair<string, object>("Role_Id", user.RoleId), new KeyValuePair<string, object>("activationCode", user.ActivationCode) });
             this.SendVerificationLinkEmail(user.Email, user.ActivationCode);
            return new JsonResult("Registration has been done,And Account activation link has been sent your email:" + user.Email);
       //     return new StatusCodeResult(200);


        }
        public void SendVerificationLinkEmail(string Email, string ActivationCode)
        {
            // creating verification url
            var verifyUrl = "/api/verify/" + ActivationCode;
            Uri request = new Uri("http://localhost:5001");
            // creating verification link
            var link = request.Scheme + "://" + request.Host.ToString() + ":" + request.Port + verifyUrl;

            // email sender mail address
            var fromEmail = new MailAddress("mobilestoreproject@mail.ru");

            // to email address
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "heraxos11";

            // messege subject
            string subject = "Confirmation of email address";

            // messege body
            string body = "<br/><br/> We are excited to tell you that your account is" +
      " successfully created. Please click on the below link to verify your account" +
      " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            // smtp client for send messege
            var smtp = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            // creating Mail massege
            using (var messege = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                // sending
                smtp.Send(messege);
        }

        public bool IsValidEmail(string email)
        {
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }


        // PUT: api/Register/5
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
