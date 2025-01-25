using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace InvoiceArchive.WebApi.Settings
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               var elasticUri = context.Configuration.GetValue<string>("ElasticDbSettings:ConnectionString");

               configuration
                            .Enrich.FromLogContext()
                            .Enrich.WithMachineName()
                            .WriteTo.Debug()
                            .WriteTo.Console()
                            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                            {
                                IndexFormat = $"{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-").Replace("ı", "i")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}",
                                AutoRegisterTemplate = true,
                                NumberOfShards = 2,
                                NumberOfReplicas = 1
                            })
                            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                            .ReadFrom.Configuration(context.Configuration);
           };
    }
}
