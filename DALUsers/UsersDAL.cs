﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALUsers
{
    public class UsersDAL
    {
        private readonly string connectionString;

        public UsersDAL()
        {
            // this.connectionString = ConfigurationManager.ConnectionStrings["UsersConnectionString"].ConnectionString;
            this.connectionString = "Data Source=(local);Initial Catalog=UsersDB;";
        }

        public void AddAdmin(string name, string login, string password, int roles_id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "AddAdmin", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Roles_ID", roles_id);
                cmd.ExecuteNonQuery();

            }
        }

        public void AddCustomer(string name, string surname, string email, string login, string password, int roles_id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "AddCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Roles_ID", roles_id);
                cmd.ExecuteNonQuery();

            }
        }

        public void AddSeller(string name, string address, string cellphone, string login, string password, int roles_id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "AddSeller", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@CellPhone", cellphone);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Roles_ID", roles_id);
                cmd.ExecuteNonQuery();

            }
        }

        public void DeleteAdmin(int id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "DeleteAdmin", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "DeleteCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSeller(int id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "DeleteSeller", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void ChangeStatus(int id, bool b)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "ChangeStatus", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Status", b);
                cmd.ExecuteNonQuery();

            }
        }

        public Admin GetAdminByID(int id)
        {
            var admin = new Admin();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "GetAdminByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admin.Id = (int)reader["Id"];
                        admin.Name = (string)reader["Name"];
                        admin.Login = (string)reader["Login"];
                        admin.Password = (string)reader["Password"];
                        admin.Roles_ID = (int)reader["Roles_ID"];
                    }
                }
                admin.AdminRole = GetRole(admin.Roles_ID);
            }
            return admin;
        }

        public Role GetRole(int id)
        {
            var role = new Role();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "GetRole", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        role.RoleId = (int)reader["Id"];
                        role.Name = (string)reader["Name"];
                        role.Description = (string)reader["Description"];
                    }
                }
            }
            return role;
        }

        public Customer GetCustomerByID(int id)
        {
            var customer = new Customer();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "GetCustomerByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer.Id = (int)reader["Id"];
                        customer.Name = (string)reader["Name"];
                        customer.Surname = (string)reader["Surname"];
                        customer.Login = (string)reader["Login"];
                        customer.Password = (string)reader["Password"];
                        customer.Roles_ID = (int)reader["Roles_ID"];
                        customer.Status = reader.GetBoolean(reader.GetOrdinal("Status"));
                    }
                }
            }
            customer.CustomerRole = GetRole(customer.Roles_ID);
            return customer;
        }

        public Seller GetSellerByID(int id)
        {
            var seller = new Seller();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "GetSellerByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seller.Id = (int)reader["Id"];
                        seller.Name = (string)reader["Name"];
                        seller.Address = (string)reader["Address"];
                        seller.CellPhone = (string)reader["CellPhone"];
                        seller.Login = (string)reader["Login"];
                        seller.Password = (string)reader["Password"];
                        seller.Roles_ID = (int)reader["Roles_ID"];
                        if (!Convert.IsDBNull(reader["Rating"]))
                        {
                            seller.Rating = Convert.ToDecimal(reader["Rating"]);
                        }
                    }
                    seller.SellerRole = GetRole(seller.Roles_ID);
                }
            }
            return seller;
        }
        public Customer GetCustomerByName(string login)
        {
            var customer = new Customer();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "GetCustomerByName", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Login", login);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer.Id = (int)reader["Id"];
                        customer.Name = (string)reader["Name"];
                        customer.Surname = (string)reader["Surname"];
                        customer.Login = (string)reader["Login"];
                        customer.Password = (string)reader["Password"];
                        customer.Roles_ID = (int)reader["Roles_ID"];
                        customer.Status = reader.GetBoolean(reader.GetOrdinal("Status"));
                    }
                }
            }
            customer.CustomerRole = GetRole(customer.Roles_ID);
            return customer;
        }
        public Seller GetSellerByName(string login)
        {
            var seller = new Seller();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var cmd = new SqlCommand(
                    "GetSellerByName", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Login", login);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seller.Id = (int)reader["Id"];
                        seller.Name = (string)reader["Name"];
                        seller.Address = (string)reader["Address"];
                        seller.CellPhone = (string)reader["CellPhone"];
                        seller.Login = (string)reader["Login"];
                        seller.Password = (string)reader["Password"];
                        seller.Roles_ID = (int)reader["Roles_ID"];
                        if (!Convert.IsDBNull(reader["Rating"]))
                        {
                            {
                                seller.Rating = Convert.ToDecimal(reader["Rating"]);
                            }
                        }
                        seller.SellerRole = GetRole(seller.Roles_ID);
                    }
                }
                               
            } return seller;
        }
            public void RateSeller(decimal rating)
            {
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "RateSeller", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Rate", rating);
                    cmd.ExecuteNonQuery();

                }
            }

            public IEnumerable<Customer> GetCustomers()
            {
                var customers = new List<Customer>();
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "GetCustomers", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.Id = (int)reader["Id"];
                            customer.Name = (string)reader["Name"];
                            customer.Surname = (string)reader["Surname"];
                            customer.Login = (string)reader["Login"];
                            customer.Password = (string)reader["Password"];
                            customer.Roles_ID = (int)reader["Roles_ID"];
                            customer.Status = reader.GetBoolean(reader.GetOrdinal("Status"));
                            customer.CustomerRole = GetRole(customer.Roles_ID);
                            customers.Add(customer);
                        }
                    }
                }
                return customers;
            }

            public IEnumerable<Seller> GetSellers()
            {
                var sellers = new List<Seller>();
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();
                    var cmd = new SqlCommand(
                        "GetSellers", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var seller = new Seller();
                            seller.Id = (int)reader["Id"];
                            seller.Name = (string)reader["Name"];
                            seller.Address = (string)reader["Address"];
                            seller.CellPhone = (string)reader["CellPhone"];
                            seller.Login = (string)reader["Login"];
                            seller.Password = (string)reader["Password"];
                            seller.Roles_ID = (int)reader["Roles_ID"];
                            if (!Convert.IsDBNull(reader["Rating"]))
                            {
                                seller.Rating = Convert.ToDecimal(reader["Rating"]);
                            }
                            seller.SellerRole = GetRole(seller.Roles_ID);
                            sellers.Add(seller);
                        }
                    }
                }
                return sellers;
            }

            public void UpdateSeller(string login, string name, string address, string cellphone, string password)
            {
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "UpdateSeller", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@CellPhone", cellphone);
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();

                }
            }

            public void UpdateCustomer(string login, string name, string surname, string email, string password)
            {
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "UpdateCustomer", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Surname", surname);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();

                }
            }
            public bool LoginExistsCustomer(string login)
            {
                int x = 0;
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "LoginExistsCustomer", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Login", login);
                    x = (int)cmd.ExecuteScalar();
                    if (x == 2) return false;

                }
                return true;
            }

            public bool LoginExistsSeller(string login)
            {
                int x = 0;
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "LoginExistsSeller", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Login", login);
                    x = (int)cmd.ExecuteScalar();
                    if (x == 2) return false;

                }
                return true;
            }

            public void UpdateAdmin(int id, string name, string login, string password)
            {
                using (var connection = new SqlConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var cmd = new SqlCommand(
                        "UpdateAdmin", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }