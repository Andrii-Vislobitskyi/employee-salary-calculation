using Dapper;
using Npgsql;
using System.Data;
using EMP.Common.Configuration;
using EMP.DataAccess.Models;

namespace EMP.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(EmployeeAPIConfig config) 
        {
            _connectionString = config.ConnectionString;
        }
        public async Task<User?> FindUserByPhone_Async(string phoneNumber)
        {    
            var parameters = new { Phone = phoneNumber };
         
            var querySQL = @"SELECT id, externalid, phone, passwordhash, firstname,
                                    lastname, patronymic, isactive, createdat
	                         FROM public.users
                             WHERE phone = @Phone;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var users = await connection.QueryAsync<User>(querySQL, parameters);

                return users.FirstOrDefault();
            }
        }


        public async Task<User?> FindUser_ByExternalID_Async(int externalId)
        {
            var parameters = new { ExternalId = externalId };

            var querySQL = @"SELECT id, externalid, phone, passwordhash, firstname,
                                    lastname, patronymic, isactive, createdat
	                         FROM public.users
                             WHERE externalid = @ExternalId ;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var users = await connection.QueryAsync<User>(querySQL, parameters);

                return users.FirstOrDefault();
            }

        }


        public async Task<User?> FindUserByFullName_Async(string firstName, string lastName, string patronymic)
        {
            var parameters = new { FirstName = firstName, LastName = lastName, Patronymic = patronymic };

            var querySQL = "";

            using(var connection = new NpgsqlConnection(_connectionString))
            {
                var users = await connection.QueryAsync<User>(querySQL,parameters);

                return users.FirstOrDefault();
            }
        }

        public async Task UpdateUserByPhone_Async(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Phone", user.Phone, DbType.String);

            var querySQL = "";

            using (var connection  = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(querySQL,parameters);
            }
        }

        public async Task CreateUser_Async(User user)
        {
            var querySQL = @"INSERT INTO public.users(
	                                externalid, phone, passwordhash, firstname, lastname
                                  , patronymic, isactive, createdat)
	                               VALUES (@ExternalIdId, @Phone, @PasswordHash, @FirstName, @LastName
                                  , @patronymic, @IsActive, @CreatedAt)";

            using(var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(querySQL, user);
            }
        }

        public async Task Create_Table_Async(string sqlQuery)
        {
           using (var connection =new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
            }
        }
    }
}
