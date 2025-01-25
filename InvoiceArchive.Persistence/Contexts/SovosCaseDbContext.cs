using InvoiceArchive.Domain.Entities.Sql;
using InvoiceArchive.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InvoiceArchive.Persistence.Contexts
{
    public class InvoiceArchiveDbContext : DbContext
    {
        public InvoiceArchiveDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<InvoiceSql> Invoices { get; set; }
        public DbSet<InvoiceItemSql> InvoiceItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(InvoiceItemSqlSettings)));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntitySql>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                    data.Entity.CreatedOn = DateTime.UtcNow;
                else if (data.State == EntityState.Modified)
                    data.Entity.ModifiedOn = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
