using PhamNguyenTrongTuanMVC.Helpers;
using PhamNguyenTrongTuanMVC.Models.Account;
using RepositoryLayer.Accounts;
using RepositoryLayer.Categories;
using RepositoryLayer.NewsArticles;
using RepositoryLayer.Tags;
using ServiceLayer.Account;
using ServiceLayer.Category;
using ServiceLayer.NewsArticle;
using ServiceLayer.Tag;

namespace PhamNguyenTrongTuanMVC.Extentions
{
    public static class DependencyInjectionExtentions
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddAutoMapper(
                opt =>
                {
                    opt.AddProfile<MappingProfile>();
                },
                typeof(AppDomain)
            );
            services.Configure<AdminOptions>(config.GetSection(AdminOptions.Admin));
            services.Configure<PaginationOptions>(config.GetSection(PaginationOptions.Pagination));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITagRepository, TagRepository>();
            return services;
        }
    }
}
