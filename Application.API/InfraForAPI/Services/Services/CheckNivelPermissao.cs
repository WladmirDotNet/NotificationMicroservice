using BLLCore.Services;
using Microsoft.AspNetCore.Authorization;
using UtilCore.Util;

namespace InfraForAPI.Services.Services
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CheckNivelPermissao : AuthorizeAttribute
    {
        /// <summary>
        /// Pega as regras de autorizações
        /// </summary>
        /// <param name="roles"></param>

        public CheckNivelPermissao(params RegraSistemaCore.TipoRegraSistema[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            Roles = string.Join(",", roles.Select(r => r.GetIntValue()));
        }

    }
}
