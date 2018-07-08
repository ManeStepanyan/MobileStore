using System.Collections.Generic;
using System.Linq;
using DALUsers;
using AutoMapper;
using BLDALMapper;

namespace BusinessLayer
{
    public class UsersRepository
    {
        private UsersDAL dal;

        public object Map { get; private set; }

        public UsersRepository()
        {
            this.dal = new UsersDAL();
        }

        public bool SellerSignUp(Seller seller)
        {
            if (CheckSellerLogin(seller.Login))
            {
                return false;
            }

            this.dal.AddSeller(
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
            this.dal.DeleteSeller(id);
            ///Ստեղ կարելա քեշավորել ու պահել որոշակի ժամկետով անվտանգության նկատառումներով
            return true;
        }

        public bool SellerUpdate(Seller seller)
        {
            if (!CheckSellerLogin(seller.Login))
            {
                return false;
            }

            this.dal.UpdateSeller(seller.Login,
                seller.Name,
                seller.Address, 
                seller.CellPhone, 
                seller.Password.Encrypt());
            return true;
        }

        public bool CheckSellerLogin(string login)
        {
            return this.dal.LoginExistsSeller(login);
        }

        public Seller GetSeller(int id)
        {
            return this.dal.GetSellerByID(id).ConvertToBlSeller();
        }

        public Seller GetSeller(string login)
        {
            return this.dal.GetSellerByName(login).ConvertToBlSeller();
        }

        public IEnumerable<Seller> GetAllSellers()
        {
            return this.dal.GetSellers().ConvertToBlSellerEnum();
        }

        public bool CustomerSignUp(Customer customer)
        {
            if (CheckCustomerLogin(customer.Login))
            {
                return false;
            }

            this.dal.AddCustomer(
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
            this.dal.DeleteCustomer(id);
            ///Ստեղ կարելա քեշավորել ու պահել որոշակի ժամկետով անվտանգության նկատառումներով
            return true;
        }

        public bool CustomerUpdate(Customer customer)
        {
            if (!CheckCustomerLogin(customer.Login))
            {
                return false;
            }

            this.dal.UpdateCustomer(customer.Login,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.Password.Encrypt());

            return true;
        }

        public bool CheckCustomerLogin(string login)
        {
            return this.dal.LoginExistsCustomer(login);
        }

        public Customer GetCustomer(int id)
        {
            return this.dal.GetCustomerByID(id).ConvertToBlCustomer();
        }

        public Customer GetCustomer(string login)
        {
            return this.dal.GetCustomerByName(login).ConvertToBlCustomer();
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return this.dal.GetCustomers().ConvertToBlCustomerEnam();
        }

        public bool AdminSignUp(Admin admin)
        {
            this.dal.AddAdmin(
                admin.Name,
                admin.Login,
                admin.Password,
                admin.Roles_ID);

            return true;
        }

        public bool AdminDelete(int id)
        {
            this.dal.DeleteAdmin(id);
            ///Ստեղ կարելա քեշավորել ու պահել որոշակի ժամկետով անվտանգության նկատառումներով
            return true;
        }

        public bool AdminUpdate(Admin admin)
        {
            this.dal.UpdateAdmin(
                (int)admin.Id,
                admin.Name,
                admin.Login,
                admin.Password.Encrypt());

            return true;
        }

        public Admin GetAdmin(int id)
        {
            return this.dal.GetAdminByID(id).ConvertToBlAdmin();
        }

        public Role GetRole(int id)
        {
            return this.dal.GetRole(id).ConvertToBlRole();
        }


        public bool ChangeCustomerStatus(int id, bool NewStatus)
        {
            if (!CheckCustomerLogin(GetCustomer(id).Login))
            {
                return false;
            }

            this.dal.ChangeStatus(id, NewStatus);
            return true;
        }
    }
}
