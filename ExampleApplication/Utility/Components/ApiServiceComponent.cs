namespace Example.App.Utility.Components
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
