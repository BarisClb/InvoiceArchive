using InvoiceArchive.Domain.Entities.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceArchive.Domain.Settings
{
    public class InvoiceItemSqlSettings : IEntityTypeConfiguration<InvoiceItemSql>
    {
        public void Configure(EntityTypeBuilder<InvoiceItemSql> builder)
        {
            builder.ToTable("InvoiceItems").HasKey(x => x.Id);

            builder.Property(ii => ii.Id).IsRequired();
            builder.Property(ii => ii.Name).IsRequired();
            builder.Property(ii => ii.Quantity).IsRequired();
            builder.Property(ii => ii.UnitCode).IsRequired();
            builder.Property(ii => ii.UnitPrice).IsRequired();
            builder.Property(ii => ii.CreatedOn).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ii => ii.InvoiceGuid).IsRequired();
            builder.HasOne(ii => ii.Invoice).WithMany(i => i.InvoiceItems).HasForeignKey(ii => ii.InvoiceId).HasPrincipalKey(i => i.InvoiceId);
        }
    }
}
