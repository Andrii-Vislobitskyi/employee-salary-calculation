
using EMP.DataAccess.Models;

namespace EMP.DataAccess.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Find user by phone
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        Task<User?> FindUserByPhone_Async(string phoneNumber);
        /// <summary>
        /// Find user by full name
        /// </summary>
        /// <param name="firstName">Firat name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="patronymic">Patronymic</param>
        /// <returns></returns>
        Task<User?> FindUserByFullName_Async(string firstName, string lastName, string patronymic);
        /// <summary>
        /// Insert phone value
        /// </summary>
        /// <param name="user">Entity User</param>
        /// <returns></returns>
        Task UpdateUserByPhone_Async(User user);
       /// <summary>
       /// Insert new User to Data Base
       /// </summary>
       /// <param name="user">Entity User</param>
       /// <returns></returns>
        Task CreateUser_Async(User user);
        /// <summary>
        /// Create table Users in database
        /// </summary>
        /// <param name="sqlQuery">Sql query from file</param>
        /// <returns></returns>
        Task Create_Table_Async(string sqlQuery);
        /// <summary>
        /// Find User by External ID
        /// </summary>
        /// <param name="externalId">External ID</param>
        /// <returns>Entity User</returns>
        Task<User?> FindUser_ByExternalID_Async(int externalId);
    }
}
