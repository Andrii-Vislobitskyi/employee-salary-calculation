using EMP.Common.Dto.Auth_Dto;
using EMP.DataAccess.Models;

namespace EMP.Services.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginRequest">Login DTO</param>
        /// <returns>Token</returns>
        Task<string?> LoginUser_Async(LoginRequestDTO loginRequest);
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="registerRequest">Register DTO</param>
        /// <returns>Token</returns>
        Task<string> RegisterUser_Async(RegisterRequestDTO registerRequest);
        /// <summary>
        /// Get Full Name by ID
        /// </summary>
        /// <param name="externalId">User`s ID in external API database</param>
        /// <returns>FullName</returns>
        Task<string> GetFullName_Async(int externalId);
        /// <summary>
        /// Generate application token
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="externalToken">Token from external API</param>
        /// <returns>Token</returns>
        Task<string> GenerateToken_Async(User user, string? externalToken);
    }
}
