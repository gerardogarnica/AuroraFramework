using Aurora.Framework.Platform;
using Aurora.Platform.Repositories;
using Aurora.Platform.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Platform.API.Configurations
{
    public static class PlatformConfiguration
    {
        public static IServiceCollection AddPlatformApiConfiguration(this IServiceCollection services)
        {
            // Configuración de servicios Platform Core
            services.AddPlatformConfiguration();

            // Configuración de repositorios
            services.AddPlatformRepositories();

            // Configuración de servicios de dominio
            services.AddPlatformServices();

            // Configuración de AutoMapper
            services.AddPlatformMapper();

            // Configuración de MediatR
            services.AddPlatformMediatR();

            return services;
        }
    }
}