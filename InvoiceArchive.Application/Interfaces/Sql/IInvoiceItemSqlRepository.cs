using InvoiceArchive.Domain.Entities.Sql;

namespace InvoiceArchive.Application.Interfaces.Sql
{
    public interface IInvoiceItemSqlRepository : IBaseSqlRepository<InvoiceItemSql>
    {
        Task<int> DeleteInvoiceItemsByInvoiceId(string? invoiceId = default, Guid? invoiceGuid = default);
    }
}
