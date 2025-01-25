using AutoMapper;
using InvoiceArchive.Application.Interfaces.Mongo;
using InvoiceArchive.Application.Responses;
using MediatR;

namespace InvoiceArchive.Application.Queries.GetInvoiceByIdFromRegister
{
    public class GetInvoiceByIdFromRegisterQueryHandler : IRequestHandler<GetInvoiceByIdFromRegisterQueryRequest, BaseResponse<GetInvoiceByIdFromRegisterQueryResponse>>
    {
        private readonly IInvoiceMongoRepository _invoiceMongoRepository;
        private readonly IMapper _mapper;

        public GetInvoiceByIdFromRegisterQueryHandler(IInvoiceMongoRepository invoiceMongoRepository, IMapper mapper)
        {
            _invoiceMongoRepository = invoiceMongoRepository ?? throw new ArgumentNullException(nameof(invoiceMongoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<BaseResponse<GetInvoiceByIdFromRegisterQueryResponse>> Handle(GetInvoiceByIdFromRegisterQueryRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceMongoRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
                return BaseResponse<GetInvoiceByIdFromRegisterQueryResponse>.Fail($"No Invoice found with Id: '{request.InvoiceId}'.", 404);

            return BaseResponse<GetInvoiceByIdFromRegisterQueryResponse>.Success(_mapper.Map<GetInvoiceByIdFromRegisterQueryResponse>(invoice), 200);
        }
    }
}
