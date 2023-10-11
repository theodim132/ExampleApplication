namespace Example.App.Utility
{
    public static class BasicAppConfiguration
    {
        public static IServiceCollection AddBasicServices(this IServiceCollection services) 
        {
            services.AddControllers();
            return services;
        } 
    }
}
