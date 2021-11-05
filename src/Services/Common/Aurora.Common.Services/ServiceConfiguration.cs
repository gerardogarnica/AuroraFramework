using Aurora.Common.Services.Catalogs.Queries;
using Aurora.Common.Services.Locations.Queries;
using Aurora.Common.Services.Mappers;
using Aurora.Common.Services.Settings.Queries;
using Aurora.Framework.Sessions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aurora.Common.Services
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            // Servicios de catálogos
            services.AddScoped<ICatalogQueryService, CatalogQueryService>();

            // Servicios de localidades
            services.AddScoped<ICountryQueryService, CountryQueryService>();
            services.AddScoped<ILocationQueryService, LocationQueryService>();

            // Servicios de parámetros
            services.AddScoped<IAttributeSettingQueryService, AttributeSettingQueryService>();
            services.AddScoped<IAttributeValueQueryService, AttributeValueQueryService>();

            return services;
        }

        public static IServiceCollection AddCommonMapper(this IServiceCollection services)
        {
            services.AddScoped(f => new MapperConfiguration(x =>
            {
                x.AddProfile(new CommonMapperProfile(f.GetService<IAuroraSession>()));
            }).CreateMapper());

            return services;
        }

        public static IServiceCollection AddCommonMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}