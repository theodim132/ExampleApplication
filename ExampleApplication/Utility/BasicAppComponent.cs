namespace Example.App.Utility
{
    public static class BasicAppComponent
    {
        public static IServiceCollection AddBasicServices(this IServiceCollection services) 
        {
            services.AddControllers();
            return services;
        } 
    }
}
