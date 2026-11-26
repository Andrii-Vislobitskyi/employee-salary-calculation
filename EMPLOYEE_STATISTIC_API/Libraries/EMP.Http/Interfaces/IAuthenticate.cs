using EMP.Common.Dto.Auth_Dto;

namespace EMP.Http.Interfaces
{
    public interface IAuthenticate
    {
        /// <summary>
        /// Get token fom external API
        /// </summary>
        /// <param name="cardCodeDTO">Data type</param>
        /// <returns>Token</returns>
        Task<string?> AuthenticateAsync(UserCardCodeDTO cardCodeDTO);
    }
}
