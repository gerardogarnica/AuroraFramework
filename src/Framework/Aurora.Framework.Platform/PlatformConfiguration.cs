using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de acceso a servicios comunes de Aurora Platform.
    /// </summary>
    public static class PlatformConfiguration
    {
        /// <summary>
        /// Agrega la configuración de acceso a servicios comunes de Aurora Platform para una interface IServiceCollection especificada.
        /// </summary>
        /// <param name="services">Especifa la interface Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// donde se agregará la configuración de acceso a servicios comunes de Aurora Platform.</param>
        public static IServiceCollection AddPlatformConfiguration(this IServiceCollection services)
        {
            services.AddProxyServices();

            services.AddScoped<IApplicationsServices, ApplicationsServices>();
            services.AddScoped<ICatalogsServices, CatalogsServices>();
            services.AddScoped<IComponentsServices, ComponentsServices>();
            services.AddScoped<ICountriesServices, CountriesServices>();
            services.AddScoped<IIdentityServices, IdentityServices>();
            services.AddScoped<ILocationsServices, LocationsServices>();
            services.AddScoped<IRepositoriesServices, RepositoriesServices>();
            services.AddScoped<IRolesServices, RolesServices>();
            services.AddScoped<ISettingsServices, SettingsServices>();
            services.AddScoped<IUsersServices, UsersServices>();

            return services;
        }
    }
}