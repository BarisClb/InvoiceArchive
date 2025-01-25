using InvoiceArchive.Domain.Entities.Mongo;

namespace InvoiceArchive.Application.Commands.CreateInvoiceRegister
{
    public class CreateInvoiceRegisterCommandResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public ICollection<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}
