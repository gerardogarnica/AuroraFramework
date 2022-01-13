using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase con métodos de extensión para la configuración de servicios de Log utilizando Serilog.
    /// </summary>
    public static class LoggerHandlerConfiguration
    {
        /// <summary>
        /// Realiza la configuración de Serilog.
        /// </summary>
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               var elasticsearchUri = new Uri(context.Configuration.GetValue<string>("ElasticsearchConfiguration:Uri"));
               var elasticsearchIndexName = $"{context.Configuration.GetValue<string>("ElasticsearchConfiguration:IndexName")}-{DateTime.UtcNow.ToString(DateFormat.YearMonth, "-")}";

               configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUri)
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = elasticsearchIndexName,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                })
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .ReadFrom.Configuration(context.Configuration);
           };
    }
}