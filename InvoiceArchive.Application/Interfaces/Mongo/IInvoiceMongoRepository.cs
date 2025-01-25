using InvoiceArchive.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace InvoiceArchive.Application.Interfaces.Mongo
{
    public interface IInvoiceMongoRepository : IBaseMongoRepository<InvoiceMongo>
    {
        Task<InvoiceMongo> GetByIdAsync(string id);
        Task<InvoiceMongo> UpdateAndGetByIdAsync(string invoiceId, UpdateDefinition<InvoiceMongo> updateDefination);
    }
}
