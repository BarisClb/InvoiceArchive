using MediatR;

namespace InvoiceArchive.Application.Queries.GetInvoicesToStoreFromRegister
{
    public class GetInvoicesToStoreFromRegisterQueryRequest : IRequest<List<GetInvoicesToStoreFromRegisterQueryResponse>>
    { }
}
