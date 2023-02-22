using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UtilCore.Util;

namespace InfraForGlobal.Services.MassTransitRabbitMQService.Service
{
    /// <summary>
    /// Classe responsável pelo consumer do RabbitMQ
    /// </summary>
    public static class MassTransitRabbitMqService
    {
        
        /// <summary>
        /// Adiciona consumer RabbitMQ para aplicação
        /// </summary>
        /// <param name="serices"></param>
        /// <param name="configuration"></param>
        /// <param name="fila"></param>
        /// <param name="interval"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddMassTransitRabbitMqConsumer<T>(this IServiceCollection serices,IConfiguration configuration, string fila, int interval = 100) where T : class, IConsumer
        {
            var connectionsStringRabbitMq = configuration["RabbitMQ:ConnectionString"];
            var userRabbitMq = configuration["RabbitMQ:UserName"];
            var passwordRabbitMq = configuration["RabbitMQ:Password"];
            if (connectionsStringRabbitMq.IsNullOrWhiteSpace())
                throw new InvalidOperationException("RabbitMQ:ConnectionString");
            if (userRabbitMq.IsNullOrWhiteSpace())
                throw new InvalidOperationException("RabbitMQ:UserName");
            if (passwordRabbitMq.IsNullOrWhiteSpace())
                throw new InvalidOperationException("RabbitMQ:Password");

            serices.AddMassTransit(o =>
            {
                o.AddConsumer<T>();
                o.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(connectionsStringRabbitMq!), h =>
                    {
                        h.Username(userRabbitMq);
                        h.Password(passwordRabbitMq);
                    });
                    cfg.ReceiveEndpoint(fila, ep =>
                    {
                        ep.PrefetchCount = 10;
                        ep.UseMessageRetry(r => r.Interval(5, interval));
                        ep.ConfigureConsumer<T>(provider);
                    });
                }));
            });
        }
    }
}
