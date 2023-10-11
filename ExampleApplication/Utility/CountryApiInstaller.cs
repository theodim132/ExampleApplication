using Microsoft.Extensions.Options;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.HttpServices;

namespace Example.App
{
    public static class CountryApiInstaller
    {
        private const string SectionPath = "Services:CountryApi";

        public static IServiceCollection AddCountryApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddOptions<CountryApiConfiguration>()
            .Bind(configuration.GetSection(SectionPath), options =>
            {
                options.BindNonPublicProperties = true;
            })
            .Services
            .AddTransient(sp => sp.GetRequiredService<IOptionsMonitor<CountryApiConfiguration>>().CurrentValue)
            .AddHttpClient<ICountryApiService, CountryApiService>((sp, c) =>
            {
                var configuration = sp.GetRequiredService<CountryApiConfiguration>();
                c.BaseAddress = configuration.BaseUri;
            });
            return services;
        }
    }
}
