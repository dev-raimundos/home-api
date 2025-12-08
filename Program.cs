using HomeApi.Infrastructure;

namespace HomeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // ORACLE WALLET LOCATION
            var walletLocation = config["Oracle:WalletLocation"];
            if (string.IsNullOrWhiteSpace(walletLocation))
            {
                throw new InvalidOperationException("Oracle:WalletLocation configuration value is missing or empty.");
            }

            var walletPath = Path.Combine(
                builder.Environment.ContentRootPath,
                walletLocation
            );

            // INFRASTRUCTURE (DB + SERVICES + REPOSITORIES)
            builder.Services.AddInfrastructure(config, walletPath);

            // CONTROLLERS
            builder.Services.AddControllers();

            var app = builder.Build();

            // HTTPS + AUTH
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            // ENDPOINTS
            app.MapControllers();

            app.Run();
        }
    }
}
