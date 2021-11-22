using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de documentación Swagger del servicio.
    /// </summary>
    public static class SwaggerConfiguration
    {
        private static readonly string SecuritySchemeName = "Bearer";

        /// <summary>
        /// Agrega la configuración de documentación Swagger para una interface IServiceCollection especificada.
        /// </summary>
        /// <param name="services">Especifa la interface Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// donde se agregará la configuración de documentación Swagger.</param>
        /// <param name="applicationName">Nombre de la aplicación. Se incluye en el título del documento.</param>
        /// <param name="moduleName">Nombre del módulo. Se incluye en el título del documento.</param>
        /// <param name="serviceDescription">Descripción breve del servicio.</param>
        /// <param name="versionNumber">Número de versión que identifica el servicio.</param>
        public static IServiceCollection AddSwaggerServices(
            this IServiceCollection services, string applicationName, string moduleName,
            string serviceDescription, int versionNumber)
        {
            var version = string.Format("v{0}", versionNumber);

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc(version, CreateOpenApiInfo(applicationName, moduleName, serviceDescription, version));
                a.AddSecurityDefinition(SecuritySchemeName, CreateOpenApiSecurityScheme());
                a.AddSecurityRequirement(CreateOpenApiSecurityRequirement());
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddMvc().AddNewtonsoftJson(a =>
            {
                a.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            return services;
        }

        /// <summary>
        /// Agrega el registro de la configuración de documentación Swagger para una interface IApplicationBuilder especificada.
        /// </summary>
        /// <param name="app">Especifa la interface Microsoft.AspNetCore.Builder.IApplicationBuilder
        /// donde se agregará la configuración de documentación Swagger.</param>
        /// <param name="applicationName">Nombre de la aplicación. Se incluye en el título y la URI del documento.</param>
        /// <param name="moduleName">Nombre del módulo. Se incluye en el título y la URI del documento.</param>
        /// <param name="versionNumber">Número de versión que identifica el servicio.</param>
        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder app, string applicationName, string moduleName, int versionNumber)
        {
            var version = string.Format("v{0}", versionNumber);
            var documentUri = string.Format("/{0}/api/{1}/{2}/{1}.json", applicationName.Replace(" ", ""), moduleName.Replace(" ", ""), version);
            var routeTemplate = string.Format("{0}/api/{1}/{2}/{1}.json", applicationName.Replace(" ", ""), moduleName.Replace(" ", ""), "{documentName}");

            app.UseSwagger(a =>
            {
                a.RouteTemplate = routeTemplate.ToLower();
            });

            app.UseSwaggerUI(a =>
            {
                a.DocumentTitle = string.Format("{0} {1} API", applicationName, moduleName);
                a.SwaggerEndpoint(documentUri.ToLower(), string.Format("{0} {1} {2}", applicationName, moduleName, version));
            });

            return app;
        }

        private static OpenApiInfo CreateOpenApiInfo(
            string applicationName, string moduleName, string serviceDescription, string version)
        {
            return new OpenApiInfo()
            {
                Title = string.Format("{0} {1} API", applicationName, moduleName),
                Description = serviceDescription,
                Version = version,
                Contact = new OpenApiContact
                {
                    Name = "Aurora Support",
                    Email = "soporte@aurorasoft.ec"
                }
            };
        }

        private static OpenApiSecurityScheme CreateOpenApiSecurityScheme()
        {
            return new OpenApiSecurityScheme()
            {
                BearerFormat = "JWT",
                Description = "Cabecera de autorización JWT usando el esquema Bearer.\r\n\r\n" +
                    "Ingrese el término 'Bearer' [space] y el token de seguridad.\r\n\r\n" +
                    "Ejemplo: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = SecuritySchemeName
            };
        }

        private static OpenApiSecurityRequirement CreateOpenApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SecuritySchemeName
                        },
                        Scheme = "oauth2",
                        Name = SecuritySchemeName,
                        In = ParameterLocation.Header
                    },
                    new System.Collections.Generic.List<string>()
                }
            };
        }
    }
}