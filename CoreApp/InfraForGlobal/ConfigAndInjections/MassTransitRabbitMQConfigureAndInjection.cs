using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UtilCore.Util;

namespace InfraForGlobal.ConfigAndInjections
{
    /// <summary>
    /// Classe relacionada a menssageria utilizando MassTransit
    /// </summary>
    public static class MassTransitRabbitMq
    {
        /// <summary>
        /// injeta dependência para producer do MassTransit
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddMassTransitRabbitMqProducer(this IServiceCollection services, IConfigurationRoot configuration)
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

            services.AddMassTransit(x =>
            {
                x.AddBus(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(connectionsStringRabbitMq!), h =>
                    {
                        h.Username(userRabbitMq);
                        h.Password(passwordRabbitMq);
                    });
                }));
            });
        }
    }
}
