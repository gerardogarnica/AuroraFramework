using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de servicios de autenticación.
    /// </summary>
    public static class AuthenticationConfiguration
    {
        /// <summary>
        /// Agrega la configuración de servicios de autenticación para una interface IServiceCollection especificada.
        /// </summary>
        /// <param name="services">Especifa la interface Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// donde se agregará la configuración de servicios de autenticación.</param>
        /// <param name="configuration">Especifica la interface Microsoft.Extensions.Configuration
        /// que contiene las propiedades de configuración de una aplicación.</param>
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Se obtiene la variable de entorno SecretKey
            var secretValue = configuration.GetValue<string>("SecretKey");

            if (string.IsNullOrWhiteSpace(secretValue))
            {
                throw new Exceptions.PlatformException("No se encontró valor para el parámetro 'SecretKey'.");
            }

            // Se codifica el valor de SecretKey
            var secretKey = Encoding.ASCII.GetBytes(secretValue);

            // Se agrega la configuración de autenticación
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}