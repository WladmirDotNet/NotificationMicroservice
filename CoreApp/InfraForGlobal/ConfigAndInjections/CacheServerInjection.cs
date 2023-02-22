using InfraForGlobal.Services.CacheService.Interface;
using InfraForGlobal.Services.CacheService.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraForGlobal.ConfigAndInjections
{
    /// <summary>
    /// Classe de injeção do objeto de gestão do servidor de cache distribuído
    /// </summary>
    internal static class CacheServerInjection
    {
        /// <summary>
        /// Realiza a injeção do objeto de gestão do servidor de cache distribuído
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal static void AddDistributedCachingServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:ConnectionString"];
            });

            services.AddSingleton<ICacheServer, CacheServer>();
        }
    }
}
