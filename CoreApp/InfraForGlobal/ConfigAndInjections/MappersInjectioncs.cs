using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace InfraForGlobal.ConfigAndInjections
{
    /// <summary>
    /// Classe de injeção dos Mappers
    /// </summary>
    internal static class MappersInjectioncs
    {
        /// <summary>
        /// Método de extensão relacionado a injeção dos Mappers
        /// </summary>
        /// <param name="services"></param>
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(MappersCliente);
        }

        #region Métodos privados

        #region Cliente

        internal static void MappersCliente(IMapperConfigurationExpression map)
        {
            
        }

        #endregion

        #endregion


    }
}
