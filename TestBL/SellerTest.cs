using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace TestBL
{
    public class SellerTest
    {
        public SellerTest()
        {
            Console.WriteLine("Start Seller test.");
            int couunt = 20;
            var bl = new UsersRepository();
            /* var list = new List<Seller>();
             for (var i = 0; i < couunt; ++i)
             {
                 var seller = new Seller(null, $"name{i}", $"{i}{i}{i}", $"login{i}", $"{i}{i}{i}{i}{i}", $"{i}{i}{i}{i}{i}", i * 10);
                 list.Add(seller); ///
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
             } */
            var AllSellers = new List<Seller>(bl.GetAllSellers()); /////////////


            for (var i = 0; i < couunt / 2; ++i)
            {
                bl.SellerDelete((int)AllSellers[i].Id);

            }

            var AllSellers2 = new List<Seller>(bl.GetAllSellers());

            if (AllSellers2.Count == couunt / 2)
            {
                Console.WriteLine("Test 3 Pass.");
            }
            else
            {
                Console.WriteLine("Test 3 fail.");
            }

            bool pass = true;
            var diff = couunt / 2;
            for (var i = 0; i < diff; ++i)
            {
                if (AllSellers2[i].Name != AllSellers[i + diff].Name ||
                    AllSellers2[i].Password != AllSellers[i + diff].Password ||
                    AllSellers2[i].Login != AllSellers[i + diff].Login ||
                    AllSellers2[i].CellPhone != AllSellers[i + diff].CellPhone ||
                    AllSellers2[i].Roles_ID != AllSellers[i + diff].Roles_ID)

                {
                    pass = false;
                }
            }

            if (pass)
            {
                Console.WriteLine("Test 4 Pass.");
            }
            else
            {
                Console.WriteLine("Test 4 fail.");
            }

            foreach (var item in AllSellers2)
            {
                var newSeller = new Seller(item.Id,
                    item.Name + "1",
                    item.Password,
                    item.Login,
                    item.CellPhone + "1",
                    item.Address + "1",
                    item.Rating + 1);

                bl.SellerUpdate(newSeller);
            }

            var AllSellers3 = new List<Seller>(bl.GetAllSellers());
            pass = AllSellers2.Count == AllSellers3.Count;
            if (pass)
            {

                for (var i = 0; i < couunt; ++i)
                {
                    if (AllSellers2[i].Name + "1" != AllSellers3[i].Name ||
                        AllSellers2[i].Password != AllSellers3[i].Password ||
                        AllSellers2[i].Login != AllSellers3[i].Login ||
                        AllSellers2[i].CellPhone + "1" != AllSellers3[i].CellPhone ||
                        AllSellers2[i].Roles_ID + 1 != AllSellers3[i].Roles_ID)
                    {
                        pass = false;
                    }
                }
            }

            if (pass)
            {
                Console.WriteLine("Test 5 Pass.");
            }
            else
            {
                Console.WriteLine("Test 5 fail.");
            }
            


          Console.WriteLine("End seller tests.");
        }
    }
}
