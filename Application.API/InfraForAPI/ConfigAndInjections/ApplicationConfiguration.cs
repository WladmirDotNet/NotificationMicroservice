using InfraForAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace InfraForAPI.ConfigAndInjections
{
    /// <summary>
    /// Classe de configuração geral de aplicação
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Método de configuração geral da aplicação
        /// </summary>
        /// <param name="app"></param>
        public static void AddAppConfig(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("IsOpen");
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddleware<JwtMiddleware>();
        }
    }
}
