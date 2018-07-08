using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace TestBL
{
    public class Test1
    {
        public Test1()
        {
            int couunt = 20;
            var bl = new UsersRepository();
            var list = new List<Seller>();
            for (var i = 0; i < couunt; ++i)
            {
                var seller = new Seller(null, $"name{i}", $"{i}{i}{i}", $"login{i}", $"{i}{i}{i}{i}{i}", $"{i}{i}{i}{i}{i}", i * 10);
                list.Add(seller);
                bl.SellerSignUp(seller);
            }

            var AllSellers = new List<Seller>(bl.GetAllSellers());            
            bool pass = true;
            for (var i = 0; i < couunt; ++i)
            {
                if (list[i].Name != AllSellers[i].Name ||
                    list[i].Password != AllSellers[i].Password.Decrypt() ||
                    list[i].Login != AllSellers[i].Login ||
                    list[i].CellPhone != AllSellers[i].CellPhone ||
                    list[i].Roles_ID != AllSellers[i].Roles_ID) 
                    
                {
                    pass = false;
                }
            }
            if (pass)
            {
                Console.WriteLine("Test 1 Pass.");
            }
            else
            {
                Console.WriteLine("Test 1 fail.");
            }

            pass = true;
            for(var i = 0; i <  couunt; ++i)
            {
                var seller = bl.GetSeller($"login{i}");
                if (list[i].Name != seller.Name ||
                    list[i].Password != seller.Password.Decrypt() ||
                    list[i].Login != seller.Login ||
                    list[i].CellPhone != seller.CellPhone ||
                    list[i].Roles_ID != seller.Roles_ID)
                {
                    pass = false;
                }
            }

            if (pass)
            {
                Console.WriteLine("Test 2 Pass.");
            }
            else
            {
                Console.WriteLine("Test 2 fail.");
            }
        }
    }
}
