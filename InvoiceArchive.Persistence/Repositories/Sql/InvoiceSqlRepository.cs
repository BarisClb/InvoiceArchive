using InvoiceArchive.Application.Interfaces.Sql;
using InvoiceArchive.Domain.Entities.Sql;
using InvoiceArchive.Persistence.Contexts;

namespace InvoiceArchive.Persistence.Repositories.Sql
{
    public class InvoiceSqlRepository : BaseSqlRepository<InvoiceSql>, IInvoiceSqlRepository
    {
        public InvoiceSqlRepository(InvoiceArchiveDbContext context) : base(context)
        { }
    }
}
