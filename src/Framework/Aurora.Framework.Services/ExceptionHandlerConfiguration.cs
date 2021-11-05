using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de middleware para el manejo de excepciones.
    /// </summary>
    public static class ExceptionHandlerConfiguration
    {
        /// <summary>
        /// Agrega la configuración de middleware para el manejo de excepciones
        /// para una interface IServiceCollection especificada.
        /// </summary>
        /// <param name="services">Especifa la interface Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// donde se agregará la configuración de middleware para el manejo de excepciones.</param>
        public static IServiceCollection AddExceptionHandlerServices(this IServiceCollection services)
        {
            services.AddScoped<ExceptionHandlerMiddleware>();

            return services;
        }

        /// <summary>
        /// Agrega el registro de la configuración de middleware para el manejo de excepciones
        /// para una interface IApplicationBuilder especificada.
        /// </summary>
        /// <param name="app">Especifa la interface Microsoft.AspNetCore.Builder.IApplicationBuilder
        /// donde se agregará la configuración de middleware para el manejo de excepciones.</param>
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            return app;
        }
    }
}