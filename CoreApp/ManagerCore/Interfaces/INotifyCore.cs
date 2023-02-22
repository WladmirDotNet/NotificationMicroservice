using DTO.Notify.SendEmail.Input;

namespace BLLCore.Interfaces
{
    /// <summary>
    /// Classe responsável pelas notificações por menssagens
    /// </summary>
    public interface INotifyCore
    {
        /// <summary>
        /// Método responsável por enviar  notificações por email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SendEmail(NotifySendEmailInModel model);
    }
}
