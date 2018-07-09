using AutoMapper;
using DALUsers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BLDALMapper
{ /// <summary>
  /// Interface to provide mapping between classes (one to one mapping)
  /// </summary>
  /// <typeparam name="TS">Type of source</typeparam>
  /// <typeparam name="TD">Type of destionation</typeparam>
    public interface IMapper<TS, TD> where TS : new() where TD : new()
    {
        /// <summary>
        /// Map from source to destionation
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns></returns>
        TD Map(TS source);

        /// <summary>
        /// Map back 
        /// </summary>
        /// <param name="source">Souce object</param>
        /// <returns></returns>
        TS MapBack(TD source);
    }

    public class Mapper<TS, TD> : IMapper<TS, TD> where TS : new() where TD : new()
    {
        /// <summary>
        /// mapping from source to destionation
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns></returns>
        public TD Map(TS source)
        {
            return Helper<TS, TD>(source);
            /*var destination = new TD();
            var sourcetype = source.GetType();
            var destinationtype = destination.GetType();
            var sourceProperties = sourcetype.GetProperties();
            foreach (var item in sourceProperties)
            {
                var prop = destinationtype.GetProperty(item.Name);
                if (prop != null)
                {
                    prop.SetValue(destination, item.GetValue(source));
                }
            }
            return destination; */
        }

        /// <summary>
        /// mapping back
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns></returns>
        public TS MapBack(TD source)
        {
            return Helper<TD, TS>(source);
            /*  var destination = new TS();
              var sourcetype = source.GetType();
              var destinationtype = destination.GetType();
              var sourceProperties = sourcetype.GetProperties();
              foreach (var item in sourceProperties)
              {
                  var prop = destinationtype.GetProperty(item.Name);
                  if (prop != null)
                  {
                      prop.SetValue(destination, item.GetValue(source));
                  }
              }
              return destination; */
        }
        public T2 Helper<T1, T2>(T1 source) where T1 : new() where T2 : new()
        {
            var destination = new T2();
            var sourcetype = source.GetType();
            var destinationtype = destination.GetType();
            var sourceProperties = sourcetype.GetProperties();

            foreach (var item in sourceProperties)
            {
                var prop = destinationtype.GetProperty(item.Name);
                if (prop != null)
                {
                    prop.SetValue(destination, item.GetValue(source));
                }
            }
            return destination;
        }

    }
}
  /*  public static class Mapper
    {

        private static IMapper SellerMapper;
        private static IMapper CustomerMapper;
        private static IMapper AdminMapper;
        private static IMapper RoleMapper;
        

        static Mapper()
        {
           AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Seller, DALUsers.Seller>()
               .ForMember("Id", opt => opt.MapFrom(c => c.Id))      
               .ForMember("Name", opt => opt.MapFrom(src => src.Name))
               .ForMember("Password", opt => opt.MapFrom(src => src.Password))
               .ForMember("Roles_ID", opt => opt.MapFrom(src => src.Roles_ID))
               .ForMember("Login", opt => opt.MapFrom(src => src.Login))
               .ForMember("CellPhone", opt => opt.MapFrom(src => src.CellPhone))
               .ForMember("Address", opt => opt.MapFrom(src => src.Address))
               .ForMember("Rating", opt => opt.MapFrom(src => src.Rating))); 

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
               .ForMember("Id", opt => opt.MapFrom(c => c.Id))
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
            
            //return sellers.Select(a => new BusinessLayer.Seller(a.Id, a.Name, a.Password, a.Login, a.CellPhone, a.Address, a.Rating));
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
} */
