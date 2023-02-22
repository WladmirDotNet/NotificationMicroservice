using System.Security.Claims;
using DTO.Usuario.LoginAPI.Output;
using InfraForAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using UtilCore.Util;

namespace InfraForAPI.Services.Services
{
    /// <inheritdoc />
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        /// <param name="contextAccessor"></param>
        public LoginService(
            IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <inheritdoc />
        public GerarTokenApiOutModel UsuarioLogado()
        {
            var context = _contextAccessor.HttpContext;
            if (context is null)
                throw new ArgumentNullException("http context");

            var codUsuario = context.User.FindFirst(ClaimTypes.GivenName)?.Value;

            if (codUsuario.IsNullOrWhiteSpace())
            {
                throw new Exception("Usuário não autenticado.");
            }

            var usuario1Logado = new GerarTokenApiOutModel
            {
                CodUsuario = int.Parse(codUsuario!)
            };

            return usuario1Logado;
        }
    }
}
