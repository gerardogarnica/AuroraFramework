using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Common.Repositories.Catalogs;
using Aurora.Common.Repositories.Locations;
using Aurora.Common.Repositories.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Common.Repositories
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddCommonRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de acceso a datos
            services.AddDbContext<CommonDataContext>(
                o => o.UseSqlServer(configuration.GetConnectionString("CommonDataConnection"))
            );

            // Repositorios de catálogos
            services.AddScoped<ICatalogRepository, CatalogRepository>();

            // Repositorios de localidades
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryDivisionRepository, CountryDivisionRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            // Repositorios de parámetros
            services.AddScoped<IAttributeSettingRepository, AttributeSettingRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();

            return services;
        }
    }
}