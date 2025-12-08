using Microsoft.EntityFrameworkCore;
using HomeApi.Infrastructure.Database;

namespace HomeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // ==========================================
            // ORACLE WALLET (TNS_ADMIN)
            // ==========================================
            var walletPath = Path.Combine(builder.Environment.ContentRootPath,
                                          config["Oracle:WalletLocation"]);

            Environment.SetEnvironmentVariable("TNS_ADMIN", walletPath);

            // ==========================================
            // INFRA -> DATABASE ORACLE
            // ==========================================
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseOracle(config.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // ==========================================
            // ENDPOINT DE TESTE DE CONEXÃO
            // ==========================================
            app.MapGet("/test-db", async (AppDbContext db) =>
            {
                try
                {
                    await db.Database.OpenConnectionAsync();
                    return Results.Ok("Conexão com Oracle OK!");
                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao conectar: " + ex.Message);
                }
            });

            app.Run();
        }
    }
}
