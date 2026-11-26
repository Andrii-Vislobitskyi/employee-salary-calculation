using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EMP.Common.Configuration;

namespace Employee_API.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, EmployeeAPIConfig appConfig)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig.SecretKey))
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
