using InvoiceArchive.Domain.Entities.Mongo;

namespace InvoiceArchive.Application.Commands.UpdateInvoiceRegister
{
    public class UpdateInvoiceRegisterCommandResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public IEnumerable<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}
