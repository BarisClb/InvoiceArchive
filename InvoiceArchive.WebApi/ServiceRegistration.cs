using Hangfire;
using Hangfire.SqlServer;
using InvoiceArchive.Application.Consumers;
using InvoiceArchive.Application.Interfaces;
using MassTransit;

namespace InvoiceArchive.WebApi
{
    public static class ServiceRegistration
    {
        public static void RabbitMqServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // RabbitMq Registry

            services.AddMassTransit(mt =>
            {
                mt.AddConsumer<InvoiceStoreEventConsumer>();
                mt.UsingRabbitMq((context, config) =>
                {
                    config.Host(configuration["RabbitMqSettings:ConnectionString"], "/", host =>
                    {
                        host.Username(configuration["RabbitMqSettings:Username"]);
                        host.Password(configuration["RabbitMqSettings:Password"]);
                    });

                    config.ReceiveEndpoint("InvoiceStoreEvent", e =>
                    {
                        e.ConfigureConsumer<InvoiceStoreEventConsumer>(context);
                    });
                });
            });


            //services.AddMassTransit(config =>
            //{
            //    config.AddConsumer<InvoiceStoreConsumer>();

            //    config.UsingRabbitMq((context, conf) =>
            //    {
            //        conf.Host(configuration["RabbitMqSettings:ConnectionString"]);

            //        conf.ReceiveEndpoint("InvoiceStoreConsumer", c =>
            //        {
            //            c.ConfigureConsumer<InvoiceStoreConsumer>(context);
            //        });
            //    });
            //});
        }

        public static void HangfireServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // Hangfire Registry
            string? hangfireConnectionString = configuration["HangfireSettings:ConnectionString"];
            services.AddHangfire(configuration => configuration.UseSimpleAssemblyNameTypeSerializer()
                                                               .UseRecommendedSerializerSettings()
                                                               .UseSqlServerStorage(hangfireConnectionString, new SqlServerStorageOptions
                                                               {
                                                                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                                                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                                                   QueuePollInterval = TimeSpan.Zero,
                                                                   UseRecommendedIsolationLevel = true,
                                                                   DisableGlobalLocks = true,
                                                                   SchemaName = "InvoiceArchiveJobs",
                                                               }));
            services.AddHangfireServer();
        }

        public static void HangfireJobRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            Hangfire.RecurringJob.AddOrUpdate<IJobService>(job => job.StoreInvoices(), configuration["HangfireSettings:StoreInvoicesCronTime"]);
        }
    }
}
