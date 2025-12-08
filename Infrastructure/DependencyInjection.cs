using HomeApi.Domains.Users.Repositories;
using HomeApi.Domains.Users.Services;
using HomeApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            string walletPath)
        {
            // ORACLE CONFIG
            Environment.SetEnvironmentVariable("TNS_ADMIN", walletPath);

            // DATABASE
            services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("DefaultConnection"))
            );

            // REPOSITORIES
            services.AddScoped<IUserRepository, UserRepository>();

            // SERVICES
            services.AddScoped<UserService>();

            return services;
        }
    }
}
