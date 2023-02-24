using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace InfraForAPI.ConfigAndInjections
{
    /// <summary>
    /// Classe para injetar as dependências genéricas das API's
    /// </summary>
    internal static class GenericInjection
    {
        /// <summary>
        /// Adiciona parâmetros básicos para API
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblyName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddApiBasic(this WebApplicationBuilder builder, string assemblyName)
        {
            #region Cors

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddCors((o => o.AddPolicy("IsOpen", build =>
                {
                    build.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                })));
            }

            #endregion Cors

            #region Arquivo de configuração da aplicação

            builder.Configuration.AddJsonFile("AppConfig/notificationmicroservice.appsettings.json", false, true);

            #endregion Arquivo de configuração da aplicação

            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));
            builder.Services.AddEndpointsApiExplorer();

            #region Swagger

            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            #endregion Swagger

            #region Token JTW

            var cryptoKey = builder.Configuration["CryptoKey"];
            if (cryptoKey is null)
                throw new ArgumentNullException(nameof(cryptoKey));

            var key = Encoding.ASCII.GetBytes(cryptoKey);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion Token JTW

            #region Serviço de ContextAccessor 

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            #endregion Serviço de ContextAccessor 
        }
        /// <summary>
        /// Clase que converte inputs de API para TimeSpan
        /// </summary>
        public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
        {
            /// <summary>
            /// Leitor
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="typeToConvert"></param>
            /// <param name="options"></param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException"></exception>
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                return TimeSpan.Parse(value ?? throw new InvalidOperationException());
            }

            /// <summary>
            /// Escritor
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="value"></param>
            /// <param name="options"></param>
            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
