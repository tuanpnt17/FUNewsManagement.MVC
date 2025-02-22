using PhamNguyenTrongTuanMVC.Mapping;
using PhamNguyenTrongTuanMVC.Models.Account;
using RepositoryLayer.Account;
using ServiceLayer.Account;

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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
