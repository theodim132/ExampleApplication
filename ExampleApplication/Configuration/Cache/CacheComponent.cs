using Example.App.Utility;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.CacheServices;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Repositories;
using MyApp.Domain.MyDomain.Services.Abstractions;
using MyApp.Domain.MyDomain.Services;
using Viva.Diagnostics;
using Viva.Enterprise.Extensions.Serialization.Json;
using Viva.Enterprise.Extensions.Serialization;

namespace Example.App.Configuration.Cache
{
    public static class CacheComponent
    {
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services
                    .AddSingleton<ICacheService, CacheService>()
                    .AddMemoryCache();
            return services;

        }
    }
}
