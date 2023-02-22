using BLLCore.Interfaces;
using DTO.Notify.SendEmail.Input;
using MassTransit;

namespace NotificationMicroserviceAPI.Consumers
{
    /// <inheritdoc />
    public class NotifySendEmailInModelConsumer : IConsumer<NotifySendEmailInModel>
    {
        private readonly INotifyCore _notifyCore;

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        /// <param name="notifyCore"></param>
        public NotifySendEmailInModelConsumer(INotifyCore notifyCore)
        {
            _notifyCore = notifyCore;
        }

        /// <inheritdoc />
        public async Task Consume(ConsumeContext<NotifySendEmailInModel> context)
        {
            await _notifyCore.SendEmail(context.Message);
        }
    }
}
