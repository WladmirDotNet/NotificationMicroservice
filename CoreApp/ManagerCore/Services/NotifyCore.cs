using DTO.Notify.SendEmail.Input;
using System.Net.Mail;
using System.Net;
using BLLCore.Interfaces;
using Microsoft.Extensions.Configuration;
using UtilCore.Util;

namespace BLLCore.Services
{
    /// <inheritdoc />
    public class NotifyCore : INotifyCore
    {
        private readonly string _fromEmail;
        private readonly string _smtp;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;
        private readonly bool _useSsl;
        private readonly bool _logSendErrosInFile;
        private const string ErrorFileName = "EmailErrors.txt";
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="configuration"></param>
        public NotifyCore(IConfiguration configuration)
        {
            try
            {
                _fromEmail = configuration["NotificationMicroservice:FromEmail"] ?? throw new NullReferenceException("NotificationMicroservice:FromEmail");
                _smtp = configuration["NotificationMicroservice:Smtp"] ?? throw new NullReferenceException("NotificationMicroservice:Smtp");

                var port = configuration["NotificationMicroservice:Port"] ?? throw new NullReferenceException("NotificationMicroservice:Port");
                if (!int.TryParse(port, out _port))
                {
                    throw new InvalidCastException("NotificationMicroservice:Port");
                }

                _user = configuration["NotificationMicroservice:User"] ?? throw new NullReferenceException("NotificationMicroservice:User");
                _password = configuration["NotificationMicroservice:Password"] ?? throw new NullReferenceException("NotificationMicroservice:Password");

                var useSsl = configuration["NotificationMicroservice:UseSSL"] ?? throw new NullReferenceException("NotificationMicroservice:UseSSL");
                _useSsl = useSsl.ToLower() == "true";

                var logSendErrosInFile = configuration["NotificationMicroservice:LogSendErrosInFile"] ?? throw new NullReferenceException("NotificationMicroservice:LogSendErrosInFile");
                _logSendErrosInFile = logSendErrosInFile.ToLower() == "true";
            }
            catch (Exception ex)
            {
                CriaLogErro(ex.Message, ex.InnerException?.Message);
            }
        }

        /// <inheritdoc />
        public async Task SendEmail(NotifySendEmailInModel model)
        {
            try
            {
                var emailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail, model.De)
                };

                emailMessage.To.Add(new MailAddress(model.ParaEmail, model.ParaNome));
                emailMessage.Subject = model.Assunto;
                emailMessage.Body = model.Mensagem;

                var smtp = new SmtpClient
                {
                    Host = _smtp,
                    Port = _port,
                    Credentials = new NetworkCredential(_user, _password),
                    EnableSsl = _useSsl,
                    UseDefaultCredentials = false
                };

                await smtp.SendMailAsync(emailMessage);
            }
            catch (Exception ex)
            {
                if (_logSendErrosInFile)
                    CriaLogErro(ex.Message, ex.InnerException?.Message);
            }

        }

        #region Métodos privados

        /// <summary>
        /// Cria ou incrementa arquivo com erros da classe
        /// </summary>
        /// <param name="descricaoErro"></param>
        /// <param name="innerException"></param>
        private void CriaLogErro(string? descricaoErro, string? innerException)
        {
            if (descricaoErro.IsNullOrWhiteSpace())
                descricaoErro = "_";
            if (innerException.IsNullOrWhiteSpace())
                innerException = "_";

            var erro = $"{DateTime.Now.ToFormatedDataHora()} =>\n\nDescrição: {descricaoErro} \n\n InnerException: {innerException}\n\n";
            var errorFile = $"{DateTime.Now.ToShortDateString().Replace("/", "_")}_{ErrorFileName}";

            File.AppendAllText(errorFile, erro);
        }

        #endregion Métodos privados

    }
}
