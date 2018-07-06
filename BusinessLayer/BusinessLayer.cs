using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALUsers;

namespace BusinessLayer
{
    public class BusinessLayer
    {
        public UsersDAL DAL { get; }

        public BusinessLayer()
        {
            this.DAL = new UsersDAL();
        }

        public bool SellerSignUp(Seller seller)
        {
            ///Avelacnel anuni stugum@
            //if (this.DAL.CheckUserName(seller.Name))
            //{
            //    return false;
            //}

            this.DAL.AddSeller(
                seller.Name,
                seller.Address,
                seller.CellPhone,
                seller.Login,
                seller.Password.Encrypt(),
                seller.Roles_ID);

            return true;
        }

        public bool SellerDelete(int id)
        {
            this.DAL.DeleteSeller(id);
            ///Ստեղ կարելա քեշավորել ու պահել որոշակի ժամկետով անվտանգության նկատառումներով
            return true;
        }

        public bool SellerUpdate(Seller seller)
        {

            return true;
        }

        public Seller GetSeller(int id)
        {
            var map = new ReflectionBasedMapper<Seller, DALUsers.Seller>();
            return map.MapBack(this.DAL.GetSellerByID(id));
        }

        public Seller GetSeller(string login)
        {
            var map = new ReflectionBasedMapper<Seller, DALUsers.Seller>();
            return map.MapBack(this.DAL.GetSellerByName(login));
        }

        public IEnumerable<Seller> GetAllSellers()
        {
            var map = new ReflectionBasedMapper<Seller, DALUsers.Seller>();
            return this.DAL.GetSellers().Select(a => map.MapBack(a));
        }

        public bool CustomerSignUp(Customer customer)
        {
            ///Avelacnel anuni stugum@
            //if (this.DAL.CheckUserName(customer.Name))
            //{
            //    return false;
            //}

            this.DAL.AddCustomer(
                customer.Name, 
                customer.Surname, 
                customer.Email,
                customer.Login, 
                customer.Password.Encrypt(), 
                customer.Roles_ID);

            return true;
        }

        public bool CustomerDelete(int id)
        {
            this.DAL.DeleteCustomer(id);
            ///Ստեղ կարելա քեշավորել ու պահել որոշակի ժամկետով անվտանգության նկատառումներով
            return true;
        }

        public bool CustomerUpdate(Seller seller)
        {

            return true;
        }

        public Customer GetCustomer(int id)
        {
            var map = new ReflectionBasedMapper<Customer, DALUsers.Customer>();
            return map.MapBack(this.DAL.GetCustomerByID(id));
        }

        public Customer GetCustomer(string login)
        {
            var map = new ReflectionBasedMapper<Customer, DALUsers.Customer>();
            return map.MapBack(this.DAL.GetCustomerByName(login));
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            var map = new ReflectionBasedMapper<Customer, DALUsers.Customer>();
            return this.DAL.GetCustomers().Select(a => map.MapBack(a));
        }

        public bool AdminSignUp(Admin admin)
        {
            ///Avelacnel anuni stugum@
            //if (this.DAL.CheckUserName(admin.Name))
            //{
            //    return false;
            //}

            this.DAL.AddAdmin(
                admin.Name,
                admin.Login,
                admin.Password,
                admin.Roles_ID);

            return true;
        }

        public bool AdminDelete(int id)
        {
            this.DAL.DeleteAdmin(id);
            ///Ստեղ կարելա քեշավորել ու պահել որոշակի ժամկետով անվտանգության նկատառումներով
            return true;
        }

        public bool AdminUpdate(Seller seller)
        {

            return true;
        }

        public Admin GetAdmin(int id)
        {
            var map = new ReflectionBasedMapper<Admin, DALUsers.Admin>();
            return map.MapBack(this.DAL.GetAdminByID(id));
        }

        //public Admin GetAdmin(string login)
        //{
        //    var map = new ReflectionBasedMapper<Admin, DALUsers.Admin>();
        //    return map.MapBack(this.DAL.GetAdminByName(login));
        //}

        public Role GetRole(int id)
        {
            var map = new ReflectionBasedMapper<Role, DALUsers.Role>();
            return map.MapBack(this.DAL.GetRole(id));
        }
    }
}
