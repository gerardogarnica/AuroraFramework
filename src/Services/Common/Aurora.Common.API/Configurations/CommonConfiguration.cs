using Aurora.Common.Repositories;
using Aurora.Common.Services;
using Aurora.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Common.API.Configurations
{
    public static class CommonConfiguration
    {
        public static IServiceCollection AddCommonApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de servicios Proxy
            services.AddProxyServices();

            // Configuración de repositorios
            services.AddCommonRepositories(configuration);

            // Configuración de servicios de dominio
            services.AddCommonServices();

            // Configuración de AutoMapper
            services.AddCommonMapper();

            // Configuración de Validators
            services.AddCommonValidators();

            // Configuración de MediatR
            services.AddCommonMediatR();

            return services;
        }
    }
}