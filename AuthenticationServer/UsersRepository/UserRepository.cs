using DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.UsersRepository
{
    public class UserRepository:IUserRepository
    {  /// <summary>
       /// Repository
       /// </summary>
        private Repo<User> repo;

        /// <summary>
        /// Creates new instance of User repository
        /// </summary>
        public UserRepository(Repo<User> repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Finds user by username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>user</returns>
        public Task<User> FindAsync(string login)
        {
            var task = new Task<User>(() =>
            {
                return (User)this.repo.ExecuteOperation("GetUserByName",
                    new[]
                    {
                        new KeyValuePair<string,object>("login",login)
                    });
            });

            task.Start();

            return task;
        }

        /// <summary>
        /// Finds user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Id</returns>
        public Task<User> FindAsync(int id)
        {
            var task = new Task<User>(() =>
            {
                return (User)this.repo.ExecuteOperation("GetUser",
                    new[]
                    {
                        new KeyValuePair<string,object>("id",id)
                    });
            });

            task.Start();

            return task;
        }
    }
}

