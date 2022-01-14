using Aurora.Platform.Domain.Applications.Repositories;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Repositories.Applications;
using Aurora.Platform.Repositories.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Platform.Repositories
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddPlatformRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de acceso a datos
            services.AddDbContext<PlatformDataContext>(
                o => o.UseSqlServer(configuration.GetConnectionString("PlatformDataConnection"))
            );

            // Repositorios de aplicaciones
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();

            // Repositorios de seguridades
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();

            return services;
        }
    }
}