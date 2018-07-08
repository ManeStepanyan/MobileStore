using AutoMapper;
using DALUsers;
using System.Collections;
using System.Collections.Generic;

namespace BLDALMapper
{
    public static class Mapper
    {

        private static IMapper SellerMapper;
        private static IMapper CustomerMapper;
        private static IMapper AdminMapper;
        private static IMapper RoleMapper;

        static Mapper()
        {
            SellerMapper = new MapperConfiguration(cfg => cfg.CreateMap<Seller, DALUsers.Seller>()
               .ForMember("Id", opt => opt.MapFrom(c => c.Id))
               .ForMember("Name", opt => opt.MapFrom(src => src.Name))
               .ForMember("Password", opt => opt.MapFrom(src => src.Password))
               .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
               .ForMember("Login", opt => opt.MapFrom(src => src.Login))
               .ForMember("CellPhone", opt => opt.MapFrom(src => src.CellPhone))
               .ForMember("Address", opt => opt.MapFrom(src => src.Address))
               .ForMember("Rating", opt => opt.MapFrom(src => src.Rating))).CreateMapper();

            CustomerMapper = new MapperConfiguration(cfg => cfg.CreateMap<Customer, DALUsers.Customer>()
               .ForMember("Id", opt => opt.MapFrom(c => c.Id))
               .ForMember("Name", opt => opt.MapFrom(src => src.Name))
               .ForMember("Password", opt => opt.MapFrom(src => src.Password))
               .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
               .ForMember("Login", opt => opt.MapFrom(src => src.Login))
               .ForMember("Surname", opt => opt.MapFrom(src => src.Surname))
               .ForMember("Email", opt => opt.MapFrom(src => src.Email))
               .ForMember("Status", opt => opt.MapFrom(src => src.Status))).CreateMapper();

            AdminMapper = new MapperConfiguration(cfg => cfg.CreateMap<Admin, DALUsers.Admin>()
               .ForMember("Id", opt => opt.MapFrom(c => c.Id))
               .ForMember("Name", opt => opt.MapFrom(src => src.Name))
               .ForMember("Password", opt => opt.MapFrom(src => src.Password))
               .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
               .ForMember("Login", opt => opt.MapFrom(src => src.Login))).CreateMapper();

            RoleMapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, DALUsers.Role>()
               .ForMember("RoleID", opt => opt.MapFrom(c => c.RoleID))
               .ForMember("Name", opt => opt.MapFrom(src => src.Name))
               .ForMember("Description", opt => opt.MapFrom(src => src.Description))).CreateMapper();
        }

        public static BusinessLayer.Admin ConvertToBlAdmin(this DALUsers.Admin admin)
        {
            return AdminMapper.Map<DALUsers.Admin, BusinessLayer.Admin>(admin);
        }

        public static BusinessLayer.Seller ConvertToBlSeller(this DALUsers.Seller seller)
        {
            return SellerMapper.Map<DALUsers.Seller, BusinessLayer.Seller>(seller);
        }

        public static BusinessLayer.Customer ConvertToBlCustomer(this DALUsers.Customer customer)
        {
            return CustomerMapper.Map<DALUsers.Customer, BusinessLayer.Customer>(customer);
        }

        public static IEnumerable<BusinessLayer.Seller> ConvertToBlSellerEnum(this IEnumerable<DALUsers.Seller> sellers)
        {
            return SellerMapper.Map<IEnumerable<DALUsers.Seller>, IEnumerable<BusinessLayer.Seller>>(sellers);
        }

        public static IEnumerable<BusinessLayer.Customer> ConvertToBlCustomerEnam(this IEnumerable<DALUsers.Customer> customers)
        {
            return SellerMapper.Map<IEnumerable<DALUsers.Customer>, IEnumerable<BusinessLayer.Customer>>(customers);
        }

        public static BusinessLayer.Role ConvertToBlRole(this DALUsers.Role role)
        {
            return RoleMapper.Map<DALUsers.Role, BusinessLayer.Role>(role);
        }
    }
}
