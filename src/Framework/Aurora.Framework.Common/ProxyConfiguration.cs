using Aurora.Framework.Proxies;
using Aurora.Framework.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de invocación de requerimientos HTTP del servicio.
    /// </summary>
    public static class ProxyConfiguration
    {
        /// <summary>
        /// Agrega la configuración de invocación de requerimientos HTTP del servicio para una interface IServiceCollection especificada.
        /// </summary>
        /// <param name="services">Especifa la interface Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// donde se agregará la configuración de invocación de requerimientos HTTP del servicio.</param>
        public static IServiceCollection AddProxyServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddHttpClient<IAuroraProxy, AuroraProxy>();
            services.AddHttpClient<IAuroraSession, AuroraSession>();

            return services;
        }
    }
}