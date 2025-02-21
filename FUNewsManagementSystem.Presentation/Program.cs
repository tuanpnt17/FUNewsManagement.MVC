using FUNewsManagementSystem.RepositoryLayer.Data;
using FUNewsManagementSystem.RepositoryLayer.SeedData;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<FUNewsDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FUNewsConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            //Seed data
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

            await app.RunAsync();
        }
    }
}
