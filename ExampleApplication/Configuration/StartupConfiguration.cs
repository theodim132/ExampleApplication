using Example.App.Configuration.Api;
using Example.App.Configuration.Cache;
using Example.App.Configuration.Country;
using Example.App.Configuration.Database;
using Example.App.Configuration.Mvc;
using Example.App.Configuration.Swagger;

namespace Example.App.Configuration
{
    public static class StartupConfiguration
    {
        public static void ConfigureAll(IServiceCollection services, WebApplicationBuilder builder)
        {
            services
                .AddDatabase(builder.Configuration)
                .AddApiServices(builder.Configuration)
                .AddSwaggerServices()
                .AddCountryComponents()
                .AddAppServices()
                .AddCache();

            var app = builder.Build();
            app.UseStandardMiddleware();
            app.UseSwaggerMiddleware();
            app.UseCountryEndPoints();
            app.UserRequestObjEndPoints();

            app.Run();
        }
    }
}
