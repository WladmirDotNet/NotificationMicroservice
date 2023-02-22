using DTO.Usuario.LoginAPI.Output;

namespace InfraForAPI.Services.Interfaces
{
    /// <summary>
    /// Interface para o ILoginService
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Método que recupera usuário logado
        /// </summary>
        /// <returns></returns>
        GerarTokenApiOutModel UsuarioLogado();
    }
}
