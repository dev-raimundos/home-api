using HomeApi.Infrastructure;
using Scalar.AspNetCore;

namespace HomeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

            builder.Services.AddOpenApi();

            builder.Services.AddInfrastructure(config);

            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference("/", options =>
                {
                    options
                        .WithTitle("Home API Documentation")
                        .WithTheme(ScalarTheme.Mars)
                        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                });
            }

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