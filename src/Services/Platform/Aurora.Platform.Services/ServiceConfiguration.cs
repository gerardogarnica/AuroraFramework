using Aurora.Framework.Logic.Validations;
using Aurora.Framework.Sessions;
using Aurora.Platform.Services.Applications.Queries;
using Aurora.Platform.Services.Mappers;
using Aurora.Platform.Services.Security.Queries;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aurora.Platform.Services
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddPlatformServices(this IServiceCollection services)
        {
            // Servicios de aplicaciones
            services.AddScoped<IApplicationQueryService, ApplicationQueryService>();
            services.AddScoped<IComponentQueryService, ComponentQueryService>();
            services.AddScoped<IProfileQueryService, ProfileQueryService>();

            // Servicios de seguridad
            services.AddScoped<IRoleQueryService, RoleQueryService>();
            services.AddScoped<IUserQueryService, UserQueryService>();

            return services;
        }

        public static IServiceCollection AddPlatformMapper(this IServiceCollection services)
        {
            services.AddScoped(f => new MapperConfiguration(x =>
            {
                x.AddProfile(new PlatformMapperProfile(f.GetService<IAuroraSession>()));
            }).CreateMapper());

            return services;
        }

        public static IServiceCollection AddPlatformValidators(this IServiceCollection services)
        {
            services.AddValidationServices();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddPlatformMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}