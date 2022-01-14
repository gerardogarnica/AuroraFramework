using Aurora.Common.API.Configurations;
using Aurora.Common.API.Utils;
using Aurora.Framework.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aurora.Common.API
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
            // Configuraci�n de servicios de la plataforma
            services.AddCommonApiConfiguration(Configuration);

            // Configuraci�n de servicios de Swagger
            services.AddSwaggerServices(
                SwaggerStrings.ApplicationName, SwaggerStrings.ModuleName,
                SwaggerStrings.Description, SwaggerStrings.Version);

            // Configuraci�n de servicios MVC
            services.AddControllers();

            // Configuraci�n de HealthChecks
            services.AddHealthChecks();

            // Configuraci�n de autenticaci�n
            services.AddAuthenticationServices(Configuration);

            // Configuraci�n de middleware de excepciones
            services.AddExceptionHandlerServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configuraci�n de Middleware
            app.UseExceptionHandlerMiddleware();

            // Configuraci�n de Swagger
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