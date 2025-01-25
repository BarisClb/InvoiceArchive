using InvoiceArchive.Application.Responses;
using MediatR;

namespace InvoiceArchive.Application.Queries.GetInvoiceByIdFromRegister
{
    public class GetInvoiceByIdFromRegisterQueryRequest : IRequest<BaseResponse<GetInvoiceByIdFromRegisterQueryResponse>>
    {
        public string InvoiceId { get; set; }
    }
}
