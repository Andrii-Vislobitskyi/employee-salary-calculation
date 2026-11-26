using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EMP.Common.Configuration;
using EMP.DataAccess.Models;

namespace EMP.Services.Auth.Services
{
    public class JwtProvider
    {
        private readonly EmployeeAPIConfig _config;
        public JwtProvider(EmployeeAPIConfig config) 
        { 
           _config = config;
        }


        public string GenerateToken(User user, string? externalToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim("ExternalToken", externalToken),
                new Claim("ExternalID",user.ExternalId.ToString())
            };


            var signInCredentials = new SigningCredentials(
                                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey))
                                   , SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(_config.TokenExpires),
                claims: claims,
                signingCredentials: signInCredentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue.ToString();
        }


        /// <summary>
        /// Cet User data from HttpContext
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <returns>Returns a tuple where 'Item1' is the User ID, 'Item2' is the External ID, 
        /// 'Item3' is the external API token</returns>
        public (int, int, string) GetClaimsUserData(HttpContext httpContext)
        {

            var id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var externalId = httpContext.User.Claims.First(c => c.Type == "ExternalID").Value;
            var externalToken  = httpContext.User.Claims.First(c => c.Type == "ExternalToken").Value;

            return (Convert.ToInt32(id), Convert.ToInt32(externalId), externalToken);
        }
    }
}
