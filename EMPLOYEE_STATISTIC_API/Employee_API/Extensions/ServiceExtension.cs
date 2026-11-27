using EMP.Common.Helpers;
using EMP.DataAccess.Repositories;
using EMP.Services.Auth.Interfaces;
using EMP.Services.Auth.Services;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report;
using EMP.Services.Services;
using EMP.Http.Interfaces;
using EMP.Http.Services;

namespace Employee_API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<DateRangeCalculator>();
            services.AddScoped<StatistcActionStorage>();
            services.AddScoped<IMathStatistic, MathService>();
            services.AddScoped<IStatisticService, StatisticService>();           
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHashService, HashService>();

            services.AddScoped<IManagerAPIService, ManagerAPIService>();

            services.AddScoped<JwtProvider>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
