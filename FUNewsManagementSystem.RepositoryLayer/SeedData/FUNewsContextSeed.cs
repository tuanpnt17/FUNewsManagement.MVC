using System.Text.Json;
using FUNewsManagementSystem.RepositoryLayer.Data;
using Microsoft.Extensions.Logging;

namespace FUNewsManagementSystem.RepositoryLayer.SeedData
{
    public class FUNewsContextSeed
    {
        public static async Task SeedAsync(FUNewsDBContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "..",
                    "FUNewsManagementSystem.RepositoryLayer",
                    "SeedData",
                    "FUNewsData.json"
                );
                var jsonData = await File.ReadAllTextAsync(path);

                // Deserialize file JSON thành đối tượng SeedDataModel
                var seedData = JsonSerializer.Deserialize<SeedDataModel>(
                    jsonData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (seedData != null)
                {
                    // Seed cho bảng SystemAccounts
                    if (seedData.SystemAccounts != null && !context.SystemAccounts.Any())
                    {
                        await context.SystemAccounts.AddRangeAsync(seedData.SystemAccounts);
                        await context.SaveChangesAsync();
                    }

                    // Seed cho bảng Categories
                    if (seedData.Categories != null && !context.Categories.Any())
                    {
                        await context.Categories.AddRangeAsync(seedData.Categories);
                        await context.SaveChangesAsync();
                    }

                    // Seed cho bảng Tags
                    if (seedData.Tags != null && !context.Tags.Any())
                    {
                        await context.Tags.AddRangeAsync(seedData.Tags);
                        await context.SaveChangesAsync();
                    }

                    // Seed cho bảng NewsArticles
                    if (seedData.NewsArticles != null && !context.NewsArticles.Any())
                    {
                        await context.NewsArticles.AddRangeAsync(seedData.NewsArticles);
                        await context.SaveChangesAsync();
                    }

                    // Seed cho bảng NewsTags
                    if (seedData.NewsTags != null && !context.NewsTags.Any())
                    {
                        await context.NewsTags.AddRangeAsync(seedData.NewsTags);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<FUNewsContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
