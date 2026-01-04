using HomeApi.Infrastructure;

namespace HomeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

            // INFRASTRUCTURE (DB + SERVICES + REPOSITORIES)
            builder.Services.AddInfrastructure(config);

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