using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.CacheServices;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Repositories;
using MyApp.Domain.MyDomain.Services.Abstractions;
using MyApp.Domain.MyDomain.Services;
using Viva.Diagnostics;
using Viva.Enterprise.Extensions.Serialization.Json;
using Viva.Enterprise.Extensions.Serialization;

namespace Example.App.Utility.Components
{
    public static class ServicesComponent
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventLogService, EventLogService>()
                    .AddTransient<ICamelCaseJsonSerializationService, CamelCaseJsonSerializationService>()
                    .AddScoped<ICountryService, CountryService>()
                    .AddSingleton<ICacheService, CacheService>()
                    .AddScoped<ICountryRepository, CountryRepository>()
                    .AddMemoryCache();
            return services;
        }
    }
}
