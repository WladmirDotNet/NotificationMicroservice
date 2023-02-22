using System.ComponentModel;
using BLLCore.Interfaces;

namespace BLLCore.Services
{
    /// <inheritdoc />
    public class RegraSistemaCore:IRegraSistemaCore
    {
        /// <summary>
        /// Enum com as regras de sistema
        /// </summary>
        /// <summary>
        /// Enum com as regras de sistema
        /// </summary>
        public enum TipoRegraSistema
        {
            /// <summary>
            /// Visualiza alterações nas configurações gerais
            /// </summary>
            [DefaultValue("Cadastro de cliente")]
            [Description("Cria, edita e visualiza clientes e suas regras de acesso")]
            CadastroDeCliente = 7
        }
    }
}
