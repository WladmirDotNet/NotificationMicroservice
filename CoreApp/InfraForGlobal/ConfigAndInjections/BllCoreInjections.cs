using BLLCore.Interfaces;
using BLLCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfraForGlobal.ConfigAndInjections
{
    /// <summary>
    /// Classe responsável pela injeção do ManagerCore (regras de negócio) e suas dependências
    /// </summary>
    internal static class BllCoreInjections
    {
        /// <summary>
        /// Método responsável pela injeção do ManagerCore (regras de negócio) e suas dependências
        /// </summary>
        internal static void AddBllCore(this IServiceCollection service)
        {
            service.AddScoped<ICritografiaCore, CritografiaCore>();
            service.AddScoped<IRegraSistemaCore, RegraSistemaCore>();
            service.AddScoped<INotifyCore, NotifyCore>();

        }
    }
}
