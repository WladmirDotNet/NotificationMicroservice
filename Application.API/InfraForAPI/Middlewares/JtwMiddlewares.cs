using System.Security.Claims;
using DTO.Usuario.LoginAPI.Output;
using InfraForGlobal.Services.CacheService.Interface;
using Microsoft.AspNetCore.Http;
using UtilCore.Util;

namespace InfraForAPI.Middlewares
{
    /// <summary>
    /// Middleware de JTW
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheServer _cacheServer;

        /// <summary>
        /// Contrutor 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="cacheServer"></param>
        public JwtMiddleware(
            RequestDelegate next,
            ICacheServer cacheServer)
        {
            _next = next;
            _cacheServer = cacheServer;
        }

        /// <summary>
        /// invocador do middleare
        /// </summary>
        /// <param name="context"></param>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!await _cacheServer.CanConnectServer())
            {
                context.Response.StatusCode = 401;
                return;
            }

            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if (!authorizationHeader.IsNullOrWhiteSpace())
            {
                if (authorizationHeader.StartsWith("Bearer "))
                {
                    var givenNameClaim = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName);
                    if (givenNameClaim == null)
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }

                    var usuarioLoginApiOutModel = await _cacheServer.GetCacheAsync<GerarTokenApiOutModel>($"CodUsuarioLogin:{givenNameClaim.Value}");
                    if (usuarioLoginApiOutModel == null)
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }

                    var tokenInformado = authorizationHeader.Substring(7);

                    if (tokenInformado == usuarioLoginApiOutModel.Token)
                        await _next(context);
                    else
                        context.Response.StatusCode = 401;
                }
                else
                    context.Response.StatusCode = 401;
            }
            else
                await _next(context);
        }
    }
}
