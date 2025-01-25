using InvoiceArchive.Application.Interfaces;
using InvoiceArchive.Application.Services;
using InvoiceArchive.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvoiceArchive.Application
{
    public static class ServiceRegistration
    {
        public static void ApplicationServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddScoped<IInvoiceSqlService, InvoiceSqlService>();
            services.AddScoped<IJobService, JobService>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.Configure<MsSqlDbSettings>(configuration.GetSection("MsSqlDbSettings"));
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
        }
    }
}
