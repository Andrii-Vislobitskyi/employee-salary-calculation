using Newtonsoft.Json.Linq;

namespace EMP.Http.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Get users by external API
        /// </summary>
        /// <param name="token"></param>
        /// <returns>JObject</returns>
        Task<JObject?> GetExternalUsers_Async(string? token);
    }
}
