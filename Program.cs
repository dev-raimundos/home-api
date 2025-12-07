using HomeApi.Domains.Users.Controllers;
using HomeApi.Domains.Users.Repositories;
using HomeApi.Domains.Users.Services;

namespace HomeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ----- DEPENDENCY INJECTION -----
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();

            // ----- MVC CONTROLLERS -----
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(UsersController).Assembly); // registra controllers do módulo Users

            // ----- OPENAPI -----
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // ----- DEVELOPMENT ONLY -----
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // ----- MIDDLEWARE PIPELINE -----
            app.UseHttpsRedirection();

            app.UseAuthorization(); // se tiver JwtMiddleware, entra antes disso

            app.MapControllers();

            app.Run();
        }
    }
}
