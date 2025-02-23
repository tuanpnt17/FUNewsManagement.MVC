using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.SeedData;

namespace PhamNguyenTrongTuanMVC.Extentions
{
    public static class DatabaseExtentions
    {
        public static IServiceCollection AddDatabaseConfiguration(
            this IServiceCollection service,
            IConfiguration config
        )
        {
            service.AddDbContext<FUNewsDBContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });
            return service;
        }

        public static async Task<WebApplication> SeedNewData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = serviceProvider.GetRequiredService<FUNewsDBContext>();
                await context.Database.MigrateAsync();
                await FUNewsContextSeed.SeedAsync(context, loggerFactory); //seed data from json
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }
            return app;
        }
    }
}
