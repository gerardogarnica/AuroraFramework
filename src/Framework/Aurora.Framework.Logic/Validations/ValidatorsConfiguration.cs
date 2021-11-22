using Aurora.Framework.Logic.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Logic.Validations
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de validadores de modelos de datos de entradas de requerimientos.
    /// </summary>
    public static class ValidatorsConfiguration
    {
        /// <summary>
        /// Agrega la configuración de validadores de modelos de datos de entradas de requerimientos
        /// para una interface IServiceCollection especificada.
        /// </summary>
        /// <param name="services">Especifa la interface Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// donde se agregará la configuración de documentación Swagger.</param>
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}