using InvoiceArchive.Application.Interfaces.Mongo;
using InvoiceArchive.Application.Settings;
using InvoiceArchive.Domain.Entities.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InvoiceArchive.Persistence.Repositories.Mongo
{
    public class InvoiceMongoRepository : BaseMongoRepository<InvoiceMongo>, IInvoiceMongoRepository
    {
        public InvoiceMongoRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings.Value.ConnectionString, databaseSettings.Value.DatabaseName, databaseSettings.Value.InvoiceCollectionName)
        { }


        public async Task<InvoiceMongo> GetByIdAsync(string id)
        {
            return await (await _collection.FindAsync(x => x.InvoiceHeader.InvoiceId.Equals(id))).FirstOrDefaultAsync();
        }

        public async Task<InvoiceMongo> UpdateAndGetByIdAsync(string invoiceId, UpdateDefinition<InvoiceMongo> updateDefination)
        {
            return await _collection.FindOneAndUpdateAsync<InvoiceMongo>(x => x.InvoiceHeader.InvoiceId.Equals(invoiceId), updateDefination, new FindOneAndUpdateOptions<InvoiceMongo> { ReturnDocument = ReturnDocument.After });
        }
    }
}
