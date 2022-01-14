using Aurora.Framework.Platform;
using Aurora.Platform.Repositories;
using Aurora.Platform.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Platform.API.Configurations
{
    public static class PlatformConfiguration
    {
        public static IServiceCollection AddPlatformApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de servicios Platform Core
            services.AddPlatformConfiguration();

            // Configuración de repositorios
            services.AddPlatformRepositories(configuration);

            // Configuración de servicios de dominio
            services.AddPlatformServices();

            // Configuración de AutoMapper
            services.AddPlatformMapper();

            // Configuración de Validators
            services.AddPlatformValidators();

            // Configuración de MediatR
            services.AddPlatformMediatR();

            return services;
        }
    }
}