using Example.App.Utility.Providers;
using MyApp.Domain.MyDomain.Providers.Country;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;

namespace Example.App.Utility.Components
{
    public static class ProvidersComponent
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddCountryProviders();
            return services;
        }
    }
}
