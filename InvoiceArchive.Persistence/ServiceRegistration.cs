using InvoiceArchive.Application.Interfaces.Mongo;
using InvoiceArchive.Application.Interfaces.Sql;
using InvoiceArchive.Persistence.Contexts;
using InvoiceArchive.Persistence.Repositories.Mongo;
using InvoiceArchive.Persistence.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceArchive.Persistence
{
    public static class ServiceRegistration
    {
        public static void PeristenceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InvoiceArchiveDbContext>(options => options.UseSqlServer(configuration["MsSqlDbSettings:ConnectionString"]));
            services.AddScoped<IInvoiceMongoRepository, InvoiceMongoRepository>();
            services.AddScoped<IInvoiceSqlRepository, InvoiceSqlRepository>();
            services.AddScoped<IInvoiceItemSqlRepository, InvoiceItemSqlRepository>();
        }
    }
}
