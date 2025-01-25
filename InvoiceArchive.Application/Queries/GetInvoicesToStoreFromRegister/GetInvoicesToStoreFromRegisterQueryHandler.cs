using AutoMapper;
using InvoiceArchive.Application.Interfaces.Mongo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoiceArchive.Application.Queries.GetInvoicesToStoreFromRegister
{
    public class GetInvoicesToStoreFromRegisterQueryHandler : IRequestHandler<GetInvoicesToStoreFromRegisterQueryRequest, List<GetInvoicesToStoreFromRegisterQueryResponse>>
    {
        private readonly IInvoiceMongoRepository _invoiceMongoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoicesToStoreFromRegisterQueryHandler> _logger;

        public GetInvoicesToStoreFromRegisterQueryHandler(IInvoiceMongoRepository invoiceMongoRepository, IMapper mapper, ILogger<GetInvoicesToStoreFromRegisterQueryHandler> logger)
        {
            _invoiceMongoRepository = invoiceMongoRepository ?? throw new ArgumentNullException(nameof(invoiceMongoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<List<GetInvoicesToStoreFromRegisterQueryResponse>> Handle(GetInvoicesToStoreFromRegisterQueryRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceMongoRepository.GetWhereAsync(i => !i.IsStored);
            if (invoices == null)
            {
                _logger.LogError($"Failed to GetInvoicesToStoreFromRegister");
                return default;
            }
            return _mapper.Map<List<GetInvoicesToStoreFromRegisterQueryResponse>>(invoices);
        }
    }
}
