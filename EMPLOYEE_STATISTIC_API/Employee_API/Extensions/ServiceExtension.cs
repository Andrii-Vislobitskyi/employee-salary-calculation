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

            services.AddTransient<DateRangeCalculator>();
            services.AddTransient<StatistcActionStorage>();
            services.AddTransient<IMathStatistic, MathService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHashService, HashService>();

            services.AddScoped<IManagerAPIService, ManagerAPIService>();

            services.AddScoped<JwtProvider>();


            return services;
        }
    }
}
