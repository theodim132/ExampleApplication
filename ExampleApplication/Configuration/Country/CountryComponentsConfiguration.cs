namespace Example.App.Configuration.Country
{
    public static class CountryComponentsConfiguration
    {
        public static IServiceCollection AddCountryComponents(this IServiceCollection services)
        {
            services.AddCountryServices()
                    .AddCountryProviders()
                    .AddCountryFactories()
                    .AddCountryHanlders();
            return services;
        }
    }
}
