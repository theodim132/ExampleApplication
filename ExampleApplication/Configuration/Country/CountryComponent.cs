using Example.App.Utility;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.CacheServices;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Repositories;
using MyApp.Domain.MyDomain.Services.Abstractions;
using MyApp.Domain.MyDomain.Services;
using Viva.Diagnostics;
using Viva.Enterprise.Extensions.Serialization.Json;
using Viva.Enterprise.Extensions.Serialization;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country;
using MyApp.Domain.MyDomain.Handler;
using MyApp.Domain.MyDomain.Handlers.Abstractions;
using MyApp.Domain.MyDomain.Factory;
using MyApp.Domain.MyDomain.Factory.Abstractions;

namespace Example.App.Configuration.Country
{
    public static class CountryComponent
    {
        public static IServiceCollection AddCountryServices(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>()
                    .AddScoped<ICountryRepository, CountryRepository>();
            return services;

        }
        public static IServiceCollection AddCountryProviders(this IServiceCollection services)
        {
            services.AddScoped<ICountryApiProvider, CountryApiProvider>()
                    .AddScoped<ICountryDbProvider, CountryDbProvider>()
                    .AddScoped<ICountryCacheProvider, CountryCacheProvider>();
            return services;
        }
        public static IServiceCollection AddCountryHanlders(this IServiceCollection services)
        {
            services.AddScoped<ICountryCacheHandler, CountryCacheHandler>()
                    .AddScoped<ICountryDbHandler, CountryDbHanlder>()
                    .AddScoped<ICountryApiHandler, CountryApiHandler>();
            return services;
        }


        public static IServiceCollection AddCountryFactories(this IServiceCollection services) 
        {
            services.AddScoped<ICountryHandlerFactory, CountryHandlerFactory>();
            return services;    
        }
    }
}
