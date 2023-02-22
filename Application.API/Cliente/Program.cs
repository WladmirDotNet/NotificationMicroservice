using System.Reflection;
using InfraForAPI.ConfigAndInjections;
using InfraForGlobal.Services.MassTransitRabbitMQService.Service;
using NotificationMicroserviceAPI.Consumers;

namespace NotificationMicroserviceAPI
{
    /// <summary>
    /// Classe de inicializa��o de Aplica��o
    /// </summary>
    public class Program
    {
        /// <summary>
        /// m�todo de inicaliza��o
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var assembblyName = Assembly.GetExecutingAssembly().GetName().Name;
            if (assembblyName == null)
                throw new InvalidOperationException("GetExecutingAssembly");
            var builder = ApplicationInitalizer.Init(args, assembblyName, false);

            builder.Services.AddMassTransitRabbitMqConsumer<NotifySendEmailInModelConsumer>(builder.Configuration, "NotifyEmail");
           
            var app = builder.Build();
            app.AddAppConfig();
            app.Run();
        }
    }
}


