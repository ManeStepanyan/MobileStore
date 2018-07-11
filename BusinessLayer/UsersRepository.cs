using System.Collections.Generic;
using System.Linq;
using DALUsers;
using BLDALMapper;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UsersRepository:IUsersRepository
    {
        private UsersDAL dal;
        private readonly Mapper<DALUsers.Seller,Seller> mapperSeller;
        private readonly Mapper<DALUsers.Customer, Customer> mapperCustomer;
        private readonly Mapper<DALUsers.Admin, Admin> mapperAdmin;
        private readonly Mapper<DALUsers.Role, Role> mapperRole;

       // public object Map { get; private set; }

        public UsersRepository()
        {
            mapperSeller = new Mapper<DALUsers.Seller, Seller>();
            mapperCustomer = new Mapper<DALUsers.Customer, Customer>();
            mapperAdmin = new Mapper<DALUsers.Admin, Admin>();
            mapperRole = new Mapper<DALUsers.Role, Role>();
            this.dal = new UsersDAL();
     
        }

        public bool SellerSignUp(Seller seller)
        {
            if (CheckSellerLogin(seller.Login) || CheckCustomerLogin(seller.Login))
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
            var temp =this. mapperSeller.Map(this.dal.GetSellerByID(id));
            //   return this.dal.GetSellerByID(id).ConvertToBlSeller();
            /* var temp = this.dal.GetSellerByID(id);
             var l= AutoMapper.Mapper.Map<DALUsers.Seller, Seller>(temp);
             return l; */
            return temp;

        }

           public Seller GetSeller(string login)
          {
          return  this.mapperSeller.Map(this.dal.GetSellerByName(login));
              //return this.dal.GetSellerByName(login).ConvertToBlSeller();
          } 

            public IEnumerable<Seller> GetAllSellers()
            {
            List<Seller> result = new List<Seller>();
            // var temp = this.dal.GetSellers().ConvertToBlSellerEnum();
            var sellers = this.dal.GetSellers();
            foreach(var item in sellers)
            {
                result.Add(mapperSeller.Map(item));
            }
                return result;
            } 

        public bool CustomerSignUp(Customer customer)
        {
            if (CheckCustomerLogin(customer.Login) || CheckSellerLogin(customer.Login))
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
            // return this.dal.GetCustomerByID(id).ConvertToBlCustomer();
            return this.mapperCustomer.Map(this.dal.GetCustomerByID(id));

          } 

             public Customer GetCustomer(string login)
             {
            // return this.dal.GetCustomerByName(login).ConvertToBlCustomer();
            return this.mapperCustomer.Map(this.dal.GetCustomerByName(login));
             } 

           public IEnumerable<Customer> GetAllCustomer()
           { List<Customer> res = new List<Customer>();
            // return this.dal.GetCustomers().ConvertToBlCustomerEnam();
            var customers = this.dal.GetCustomers();
            foreach(var item in customers)
            {
                res.Add(this.mapperCustomer.Map(item));
            } return res;
           } 

        public bool AdminSignUp(Admin admin)
        {
            if (CheckCustomerLogin(admin.Login) || CheckSellerLogin(admin.Login))
            {
                return false;
            }

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
            // return this.dal.GetAdminByID(id).ConvertToBlAdmin();
            return this.mapperAdmin.Map(this.dal.GetAdminByID(id));
            } 

             public Role GetRole(int id)
             {
            // return this.dal.GetRole(id).ConvertToBlRole();
            return this.mapperRole.Map(this.dal.GetRole(id));
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

        public BaseUser FindUser(string login)
        {
            Task task1 = Task.Factory.StartNew(() => this.mapperSeller.Map(this.dal.GetSellerByName(login)));
            Task task2 = Task.Factory.StartNew(() => this.mapperCustomer.Map(this.dal.GetCustomerByName(login)));
            task1.Start();
            task2.Start();
            bool flag = true;
            while (flag)
            {
                if (task1.IsCompleted)
                    return task1.Result;
                else if(task2.IsCompleted)
                    return task2.Result;
            }
            return null;
        }

        public BaseUser FindUser(int id)
        {
            Task task1 = Task.Factory.StartNew(() => this.mapperAdmin.Map(this.dal.GetAdminByID(id)));
            Task task2 = Task.Factory.StartNew(() => this.mapperSeller.Map(this.dal.GetSellerByID(id)));
            Task task3 = Task.Factory.StartNew(() => this.mapperCustomer.Map(this.dal.GetCustomerByID(id)));

            task1.Start();
            task2.Start();
            task3.Start();
            bool flag = true;
            while (flag)
            {
                if (task1.IsCompleted)
                    return task1.Result;
                else if (task2.IsCompleted)
                    return task2.Result;
                else if (task3.IsCompleted)
                    return task3.Result;
            }
            return null;
        } 
    }
}

