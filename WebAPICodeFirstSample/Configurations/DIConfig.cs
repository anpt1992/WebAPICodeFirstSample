using Microsoft.Extensions.DependencyInjection;
using WebAPICodeFirstSample.Models;
using WebAPICodeFirstSample.Models.Repositories;
using WebAPICodeFirstSample.Services;

namespace WebAPICodeFirstSample.Configurations
{
    public class DIConfig
    {
        internal static void AddDependencies(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBaseRepository<ApplicationUser>, AccountRepository>();
        }
    }
}
