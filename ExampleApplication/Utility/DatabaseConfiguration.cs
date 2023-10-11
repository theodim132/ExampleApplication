
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Databases.MyDomain;

namespace Example.App.Utility
{
    public static class DatabaseConfiguration
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<MyDomainDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));            
            });
            return services;
        }
    }
}
