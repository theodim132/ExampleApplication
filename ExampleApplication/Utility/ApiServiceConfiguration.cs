﻿namespace Example.App.Utility
{
    public static class ApiServiceConfiguration
    {

        public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddCountryApiService(configuration);
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            return services;
        }
    }
}
