using InvoiceArchive.Application.Responses;
using MediatR;

namespace InvoiceArchive.Application.Queries.GetInvoiceHeadersFromRegister
{
    public class GetInvoiceHeadersFromRegisterQueryRequest : IRequest<BaseResponse<List<GetInvoiceHeadersFromRegisterQueryResponse>>>
    { }
}
