using Example.App.Configuration.Country;

namespace Example.App.Configuration.Api
{
    public static class ApiServiceComponent
    {

        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCountryApiService(configuration);
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            return services;
        }
    }
}
