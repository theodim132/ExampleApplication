namespace Example.App.Utility
{
    public static class StartupConfiguration
    {
        public static void ConfigureAll(IServiceCollection services, WebApplicationBuilder builder)
        {
            services
                .AddDatabase(builder.Configuration)
                .AddApiServices(builder.Configuration)
                .AddAppServices()
                .AddBasicServices()
                .AddSwaggerConfiguration();

            var app = builder.Build();
            app.UseDevelopmentConfiguration();
            app.UseStandardMiddleware();
            app.UseEndpointsConfiguration();

            app.Run();
        }
    }
}
