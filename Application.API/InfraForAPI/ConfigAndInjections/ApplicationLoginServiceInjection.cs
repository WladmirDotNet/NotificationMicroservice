using InfraForAPI.Services.Interfaces;
using InfraForAPI.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfraForAPI.ConfigAndInjections
{
    /// <summary>
    /// Injetor para serviço de login para API
    /// </summary>
    public static class ApplicationLoginServiceInjection
    {
        internal static void AddApILoginService(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}
