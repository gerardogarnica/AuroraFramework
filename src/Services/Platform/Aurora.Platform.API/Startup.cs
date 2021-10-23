using Aurora.Framework.Services;
using Aurora.Platform.API.Configurations;
using Aurora.Platform.API.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aurora.Platform.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuración de servicios de la plataforma
            services.AddPlatformApiConfiguration();

            // Configuración de servicios de Swagger
            services.AddSwaggerServices(
                SwaggerStrings.ApplicationName, SwaggerStrings.ModuleName,
                SwaggerStrings.Description, SwaggerStrings.Version);

            // Configuración de servicios MVC
            services.AddControllers();

            // Configuración de HealthChecks
            services.AddHealthChecks();

            // Configuración de autenticación
            services.AddAuthenticationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configuración de Swagger
            app.UseSwagger(SwaggerStrings.ApplicationName, SwaggerStrings.ModuleName, SwaggerStrings.Version);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHealthChecks("/health/liveness");
                endpoints.MapHealthChecks("/health/readiness");
            });
        }
    }
}