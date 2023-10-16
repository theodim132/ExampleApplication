using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country;
using MyApp.Domain.MyDomain.Handler;
using MyApp.Domain.MyDomain.Handler.Abstractions;

namespace Example.App.Utility.Providers
{
    public static class CountryHanlders
    {
        public static IServiceCollection AddCountryHanlders(this IServiceCollection services)
        {
            services.AddScoped<ICountryCacheHanlder,CountryCacheHanlder>()
                    .AddScoped<ICountryDbHanlder, CountryDbHanlder>()
                    .AddScoped<ICountryApiHanlder, CountryApiHanlder>();
            return services;
        }
    }
}
