using AutoMapper;
using InvoiceArchive.Application.Interfaces.Mongo;
using InvoiceArchive.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoiceArchive.Application.Queries.GetInvoiceHeadersFromRegister
{
    public class GetInvoiceHeadersFromRegisterQueryHandler : IRequestHandler<GetInvoiceHeadersFromRegisterQueryRequest, BaseResponse<List<GetInvoiceHeadersFromRegisterQueryResponse>>>
    {
        private readonly IInvoiceMongoRepository _invoiceMongoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoiceHeadersFromRegisterQueryHandler> _logger;

        public GetInvoiceHeadersFromRegisterQueryHandler(IInvoiceMongoRepository invoiceMongoRepository, IMapper mapper, ILogger<GetInvoiceHeadersFromRegisterQueryHandler> logger)
        {
            _invoiceMongoRepository = invoiceMongoRepository ?? throw new ArgumentNullException(nameof(invoiceMongoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<BaseResponse<List<GetInvoiceHeadersFromRegisterQueryResponse>>> Handle(GetInvoiceHeadersFromRegisterQueryRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceMongoRepository.GetWhereAsync(i => true);
            if (invoices == null)
            {
                _logger.LogError($"Failed to GetInvoiceHeadersFromRegister.");
                return BaseResponse<List<GetInvoiceHeadersFromRegisterQueryResponse>>.Fail($"Failed to Get Invoices.", 500);
            }
            return BaseResponse<List<GetInvoiceHeadersFromRegisterQueryResponse>>.Success(_mapper.Map<List<GetInvoiceHeadersFromRegisterQueryResponse>>(invoices), 200);
        }
    }
}
