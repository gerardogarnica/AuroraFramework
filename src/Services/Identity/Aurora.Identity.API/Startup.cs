using Aurora.Framework.Services;
using Aurora.Identity.API.Configurations;
using Aurora.Identity.API.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Aurora.Identity.API
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
            // Configuración de servicios de identificación
            services.AddIdentityApiConfiguration(Configuration);

            // Configuración de servicios de Swagger
            services.AddSwaggerServices(
                SwaggerStrings.ApplicationName, SwaggerStrings.ModuleName,
                SwaggerStrings.Description, SwaggerStrings.Version);

            // Configuración de servicios MVC
            services.AddControllers();

            // Configuración de HealthChecks
            services.AddHealthChecks();

            // Configuración de autenticación
            services.AddAuthenticationServices(Configuration);

            // Configuración de middleware de excepciones
            services.AddExceptionHandlerServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging(o =>
            {
                o.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configuración de Middleware
            app.UseExceptionHandlerMiddleware();

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