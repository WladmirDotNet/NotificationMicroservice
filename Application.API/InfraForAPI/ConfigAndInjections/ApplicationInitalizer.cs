using InfraForGlobal.ConfigAndInjections;
using Microsoft.AspNetCore.Builder;

namespace InfraForAPI.ConfigAndInjections
{
    /// <summary>
    /// Iinicializador de aplicação
    /// </summary>
    public static class ApplicationInitalizer
    {
        /// <summary>
        /// Método de inicalização de aplicação com opção de informar se a aplicação é producer do MassTransit, Caso a aplicação seja producer, é melhor informar true
        /// </summary>
        /// <param name="args"></param>
        /// <param name="assembblyName"></param>
        /// <param name="isMassTransitProducer"></param>
        public static WebApplicationBuilder Init(string[] args, string assembblyName, bool isMassTransitProducer)
        {
            var builder = Init(args, assembblyName);
            if (isMassTransitProducer)
                builder.Services.AddMassTransitRabbitMqProducer(builder.Configuration);

            return builder;

        }

        /// <summary>
        /// Método de inicalização de aplicação
        /// </summary>
        /// <param name="args"></param>
        /// <param name="assembblyNamer"></param>
        /// m>
        public static WebApplicationBuilder Init(string[] args, string assembblyNamer)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddApiBasic(assembblyNamer);
            builder.Services.AddGlobalDependencies(builder.Configuration);
            builder.Services.AddApILoginService();
            return builder;
        }
    }
}
