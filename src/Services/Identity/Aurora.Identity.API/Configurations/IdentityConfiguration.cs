using Aurora.Framework.Platform;
using Aurora.Platform.Repositories;
using Aurora.Platform.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Identity.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityApiConfiguration(this IServiceCollection services)
        {
            // Configuración de servicios Platform Core
            services.AddPlatformConfiguration();

            // Configuración de repositorios
            services.AddPlatformRepositories();

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