using Aurora.Common.Services.Catalogs.Mappers;
using Aurora.Common.Services.Catalogs.Queries;
using Aurora.Common.Services.Locations.Mappers;
using Aurora.Common.Services.Locations.Queries;
using Aurora.Common.Services.Settings.Mappers;
using Aurora.Common.Services.Settings.Queries;
using Aurora.Framework.Sessions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            services.AddSingleton(f => new MapperConfiguration(x =>
            {
                x.AddProfile(new CatalogMapperProfile(f.GetService<IAuroraSession>()));
            }).CreateMapper());

            services.AddSingleton(f => new MapperConfiguration(x =>
            {
                x.AddProfile(new LocationsMapperProfile(f.GetService<IAuroraSession>()));
            }).CreateMapper());

            services.AddSingleton(f => new MapperConfiguration(x =>
            {
                x.AddProfile(new SettingsMapperProfile());
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