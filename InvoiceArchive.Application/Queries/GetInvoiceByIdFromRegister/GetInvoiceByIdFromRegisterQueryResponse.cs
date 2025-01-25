using InvoiceArchive.Domain.Entities.Mongo;

namespace InvoiceArchive.Application.Queries.GetInvoiceByIdFromRegister
{
    public class GetInvoiceByIdFromRegisterQueryResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public IList<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}
