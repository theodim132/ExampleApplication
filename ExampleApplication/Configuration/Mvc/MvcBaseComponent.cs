using Example.App.Utility;
using Viva.Diagnostics;
using Viva.Enterprise.Extensions.Serialization.Json;
using Viva.Enterprise.Extensions.Serialization;
using Example.App.Services.Abstractions;
using Example.App.Services;

namespace Example.App.Configuration.Mvc
{
    public static class MvcBaseComponent
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services
                    .AddAuthorization()
                    .AddScoped<IRequestObjService, RequestObjService>()
                    .AddSingleton<IEventLogService, EventLogService>()
                    .AddTransient<ICamelCaseJsonSerializationService, CamelCaseJsonSerializationService>();
            return services;

        }
        public static WebApplication UseStandardMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            return app;
        }
    }
}
