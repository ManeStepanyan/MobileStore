using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace TestBL
{
    class Program
    {
        static void Main(string[] args)
        {
            // SellerTest t1 = new SellerTest();    
            UsersRepository repository = new UsersRepository();
          
           /* Seller seller = new Seller();
            seller.Name = "Mane";
            seller.Login="hjdfsffd";
            seller.Address = "dd787878s";
            seller.CellPhone = "ddffsssd";
            seller.Password = "mypass45w";
            repository.SellerSignUp(seller); */
            Console.WriteLine("End Test..");
            Console.Read(); 
        }
    }
}
