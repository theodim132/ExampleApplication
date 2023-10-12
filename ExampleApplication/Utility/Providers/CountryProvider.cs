﻿using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country;

namespace Example.App.Utility.Providers
{
    public static class CountryProviders
    {
        public static IServiceCollection AddCountryProviders(this IServiceCollection services)
        {
            services.AddScoped<ICountryApiProvider, CountryApiProvider>()
                    .AddScoped<ICountryDbProvider, CountryDbProvider>()
                    .AddScoped<ICountryCacheProvider, CountryCacheProvider>();
            return services;
        }
    }
}
