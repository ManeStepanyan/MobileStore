using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.UsersRepository
{

    /// <summary>
    /// Interface for users repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds user by username.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>user.</returns>
        Task<User> FindAsync(string userName);

        /// <summary>
        /// Finds user by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>user</returns>
        Task<User> FindAsync(int id);
    }

}
