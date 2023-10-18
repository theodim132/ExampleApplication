namespace Example.App.Configuration.Country
{
    public static class CountryComponentsConfiguration
    {
        public static IServiceCollection AddCountryComponents(this IServiceCollection services)
        {
            services.AddCountryServices()
                    .AddCountryProviders()
                    .AddCountryHanlders();
            return services;
        }
    }
}
