using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraForGlobal.ConfigAndInjections
{
    /// <summary>
    /// Classe de injeção de dependências globais
    /// </summary>
    public static class GlobalInjection
    {
        /// <summary>
        /// Injeta dependências blobais
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddGlobalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMappers();
            services.AddBllCore();
            services.AddDistributedCachingServer(configuration);
        }
    }
}
