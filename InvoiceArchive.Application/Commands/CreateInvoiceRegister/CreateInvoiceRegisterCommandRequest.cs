using InvoiceArchive.Application.Responses;
using InvoiceArchive.Domain.Entities.Mongo;
using MediatR;

namespace InvoiceArchive.Application.Commands.CreateInvoiceRegister
{
    public class CreateInvoiceRegisterCommandRequest : IRequest<BaseResponse<CreateInvoiceRegisterCommandResponse>>
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public ICollection<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}
