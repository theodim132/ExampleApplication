using Example.App.Utility.Providers;
using MyApp.Domain.MyDomain.Providers.Country;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;

namespace Example.App.Utility.Components
{
    public static class HanldersComponent
    {
        public static IServiceCollection AddHanlders(this IServiceCollection services)
        {
            services.AddCountryHanlders();
            return services;
        }
    }
}
