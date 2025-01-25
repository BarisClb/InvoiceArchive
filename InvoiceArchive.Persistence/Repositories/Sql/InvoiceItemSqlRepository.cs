using InvoiceArchive.Application.Interfaces.Sql;
using InvoiceArchive.Domain.Entities.Sql;
using InvoiceArchive.Persistence.Contexts;

namespace InvoiceArchive.Persistence.Repositories.Sql
{
    public class InvoiceItemSqlRepository : BaseSqlRepository<InvoiceItemSql>, IInvoiceItemSqlRepository
    {
        public InvoiceItemSqlRepository(InvoiceArchiveDbContext context) : base(context)
        { }

        public async Task<int> DeleteInvoiceItemsByInvoiceId(string? invoiceId = default, Guid? invoiceGuid = default)
        {
            if (!string.IsNullOrEmpty(invoiceId))
            {
                var invoiceLineItems = _entity.Where(il => il.InvoiceId == invoiceId);
                _entity.RemoveRange(invoiceLineItems);
            }
            else if (invoiceGuid != default)
            {
                var invoiceLineItems = _entity.Where(il => il.InvoiceGuid == invoiceGuid);
                _entity.RemoveRange(invoiceLineItems);
            }
            return await _context.SaveChangesAsync();
        }
    }
}
