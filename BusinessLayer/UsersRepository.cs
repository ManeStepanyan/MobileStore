using System.Collections.Generic;
using System.Linq;
using DALUsers;
using AutoMapper;

namespace BusinessLayer
{
    public class UsersRepository
    {
        public UsersDAL DAL { get; }
        IMapper mapper1;
        IMapper mapper2;
        IMapper mapper3;
        IMapper mapper4;

        public UsersRepository()
        {
            this.DAL = new UsersDAL();


             mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<Seller, DALUsers.Seller>()
                .ForMember("Id", opt => opt.MapFrom(c => c.Id))
                .ForMember("Name", opt => opt.MapFrom(src => src.Name))
                .ForMember("Password", opt => opt.MapFrom(src => src.Password))
                .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
                .ForMember("Login", opt => opt.MapFrom(src => src.Login))
                .ForMember("CellPhone", opt => opt.MapFrom(src => src.CellPhone))
                .ForMember("Address", opt => opt.MapFrom(src => src.Address))
                .ForMember("Rating", opt => opt.MapFrom(src => src.Rating))).CreateMapper();

             mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<Customer, DALUsers.Customer>()
                .ForMember("Id", opt => opt.MapFrom(c => c.Id))
                .ForMember("Name", opt => opt.MapFrom(src => src.Name))
                .ForMember("Password", opt => opt.MapFrom(src => src.Password))
                .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
                .ForMember("Login", opt => opt.MapFrom(src => src.Login))
                .ForMember("Surname", opt => opt.MapFrom(src => src.Surname))
                .ForMember("Email", opt => opt.MapFrom(src => src.Email))
                .ForMember("Status", opt => opt.MapFrom(src => src.Status))).CreateMapper();

             mapper3 = new MapperConfiguration(cfg => cfg.CreateMap<Admin, DALUsers.Admin>()
                .ForMember("Id", opt => opt.MapFrom(c => c.Id))
                .ForMember("Name", opt => opt.MapFrom(src => src.Name))
                .ForMember("Password", opt => opt.MapFrom(src => src.Password))
                .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
                .ForMember("Login", opt => opt.MapFrom(src => src.Login))).CreateMapper();

             mapper4 = new MapperConfiguration(cfg => cfg.CreateMap<Role, DALUsers.Role>()
                .ForMember("Id", opt => opt.MapFrom(c => c.Id))
                .ForMember("Name", opt => opt.MapFrom(src => src.Name))
                .ForMember("Description", opt => opt.MapFrom(src => src.Description))).CreateMapper(); 
        }

        public bool SellerSignUp(Seller seller)
        {
            if (CheckSellerLogin(seller.Login))
            {
                return false;
            }

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
            if (!CheckSellerLogin(seller.Login))
            {
                return false;
            }

            this.DAL.UpdateSeller(seller.Login,
                seller.Name,
                seller.Address, 
                seller.CellPhone, 
                seller.Password.Encrypt());
            return true;
        }

        public bool CheckSellerLogin(string login)
        {
            return this.DAL.LoginExistsSeller(login);
        }

        public Seller GetSeller(int id)
        {
            //var map = new ReflectionBasedMapper<Seller, DALUsers.Seller>();
            //return map.MapBack(this.DAL.GetSellerByID(id));
            return mapper1.Map<DALUsers.Seller, Seller>(this.DAL.GetSellerByID(id));
        }

        public Seller GetSeller(string login)
        {
            //var map = new ReflectionBasedMapper<Seller, DALUsers.Seller>();
            //return map.MapBack(this.DAL.GetSellerByName(login));
            return mapper1.Map<DALUsers.Seller, Seller>(this.DAL.GetSellerByName(login));
        }

        public IEnumerable<Seller> GetAllSellers()
        {
            //var map = new ReflectionBasedMapper<Seller, DALUsers.Seller>();
            //return this.DAL.GetSellers().Select(a => map.MapBack(a));
            return mapper1.Map<IEnumerable<DALUsers.Seller>, List<Seller>>(this.DAL.GetSellers());
        }

        public bool CustomerSignUp(Customer customer)
        {
            if (CheckCustomerLogin(customer.Login))
            {
                return false;
            }

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

        public bool CustomerUpdate(Customer customer)
        {
            if (!CheckCustomerLogin(customer.Login))
            {
                return false;
            }

            this.DAL.UpdateCustomer(customer.Login,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.Password.Encrypt());

            return true;
        }

        public bool CheckCustomerLogin(string login)
        {
            return this.DAL.LoginExistsCustomer(login);
        }

        public Customer GetCustomer(int id)
        {
            //var map = new ReflectionBasedMapper<Customer, DALUsers.Customer>();
            //return map.MapBack(this.DAL.GetCustomerByID(id));
            return mapper2.Map<DALUsers.Customer, Customer>(this.DAL.GetCustomerByID(id));
        }

        public Customer GetCustomer(string login)
        {
            //var map = new ReflectionBasedMapper<Customer, DALUsers.Customer>();
            //return map.MapBack(this.DAL.GetCustomerByName(login));
            return mapper2.Map<DALUsers.Customer, Customer>(this.DAL.GetCustomerByName(login));
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            //var map = new ReflectionBasedMapper<Customer, DALUsers.Customer>();
            //return this.DAL.GetCustomers().Select(a => map.MapBack(a));
            return mapper2.Map<IEnumerable<DALUsers.Customer>, List<Customer>>(this.DAL.GetCustomers());
        }

        public bool AdminSignUp(Admin admin)
        {
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

        public bool AdminUpdate(Admin admin)
        {
            this.DAL.UpdateAdmin(
                (int)admin.Id,
                admin.Name,
                admin.Login,
                admin.Password.Encrypt());

            return true;
        }

        public Admin GetAdmin(int id)
        {
            //var map = new ReflectionBasedMapper<Admin, DALUsers.Admin>();
            //return map.MapBack(this.DAL.GetAdminByID(id));
            return mapper3.Map<DALUsers.Admin, Admin>(this.DAL.GetAdminByID(id));
        }

        //public Admin GetAdmin(string login)
        //{
        //    var map = new ReflectionBasedMapper<Admin, DALUsers.Admin>();
        //    return map.MapBack(this.DAL.GetAdminByName(login));
        //}

        public Role GetRole(int id)
        {
            //var map = new ReflectionBasedMapper<Role, DALUsers.Role>();
            //return map.MapBack(this.DAL.GetRole(id));
            return mapper4.Map<DALUsers.Role, Role>(this.DAL.GetRole(id));
        }


        public bool ChangeCustomerStatus(int id, bool NewStatus)
        {
            if (!CheckCustomerLogin(GetCustomer(id).Login))
            {
                return false;
            }

            this.DAL.ChangeStatus(id, NewStatus);
            return true;
        }
    }
}
