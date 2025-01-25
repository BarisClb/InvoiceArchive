using InvoiceArchive.Domain.Entities.Mongo;

namespace InvoiceArchive.Application.Queries.GetInvoicesToStoreFromRegister
{
    public class GetInvoicesToStoreFromRegisterQueryResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public List<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}
