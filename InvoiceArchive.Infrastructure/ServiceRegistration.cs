using InvoiceArchive.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvoiceArchive.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void InfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddScoped<IEmailService, EmailService.EmailService>();
        }
    }
}
