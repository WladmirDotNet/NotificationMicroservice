namespace DTO.Usuario.LoginAPI.Output
{
    /// <summary>
    /// Classe modelo referênte a geração de Tokens JTW
    /// </summary>
    public class GerarTokenApiOutModel 
    {
        /// <summary>
        /// Código do usuário que realizou o login de API
        /// </summary>
        public int CodUsuario { get; set; }
        /// <summary>
        /// Token JTW gerado para o usuário
        /// </summary>
        public string? Token { get; set; }
    }
}
