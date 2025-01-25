using AutoMapper;
using InvoiceArchive.Application.Interfaces.Mongo;
using InvoiceArchive.Application.Responses;
using InvoiceArchive.Domain.Entities.Mongo;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace InvoiceArchive.Application.Commands.UpdateInvoiceRegister
{
    public class UpdateInvoiceRegisterCommandHandler : IRequestHandler<UpdateInvoiceRegisterCommandRequest, BaseResponse<UpdateInvoiceRegisterCommandResponse>>
    {
        private readonly IInvoiceMongoRepository _invoiceMongoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInvoiceRegisterCommandHandler> _logger;

        public UpdateInvoiceRegisterCommandHandler(IInvoiceMongoRepository invoiceRepository, IMapper mapper, ILogger<UpdateInvoiceRegisterCommandHandler> logger)
        {
            _invoiceMongoRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<UpdateInvoiceRegisterCommandResponse>> Handle(UpdateInvoiceRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var invoiceUpdateBuilder = Builders<InvoiceMongo>.Update;
            var invoiceUpdates = new List<UpdateDefinition<InvoiceMongo>>();

            if (string.IsNullOrEmpty(request.InvoiceId))
                return BaseResponse<UpdateInvoiceRegisterCommandResponse>.Fail($"No Id provided to Update Invoice.", 400);

            if (!string.IsNullOrEmpty(request.SenderTitle))
                invoiceUpdates.Add(invoiceUpdateBuilder.Set(i => i.InvoiceHeader.ReceiverTitle, request.SenderTitle));
            if (!string.IsNullOrEmpty(request.ReceiverTitle))
                invoiceUpdates.Add(invoiceUpdateBuilder.Set(i => i.InvoiceHeader.ReceiverTitle, request.ReceiverTitle));
            if (request.Date != null)
                invoiceUpdates.Add(invoiceUpdateBuilder.Set(i => i.InvoiceHeader.Date, request.Date));
            if (request.IsStored != null)
                invoiceUpdates.Add(invoiceUpdateBuilder.Set(i => i.IsStored, request.IsStored));

            if (invoiceUpdates.Count == 0)
                return BaseResponse<UpdateInvoiceRegisterCommandResponse>.Fail($"No values provided to Update Invoice. InvoiceId: '{request.InvoiceId}'.", 400);

            invoiceUpdates.Add(invoiceUpdateBuilder.Set(i => i.ModifiedOn, DateTime.UtcNow));
            var updatedInvoice = await _invoiceMongoRepository.UpdateAndGetByIdAsync(request.InvoiceId, invoiceUpdateBuilder.Combine(invoiceUpdates));

            if (updatedInvoice == null)
            {
                _logger.LogError($"Failed to update Invoice in Register. Id: {request.InvoiceId}");
                return BaseResponse<UpdateInvoiceRegisterCommandResponse>.Fail($"Failed to Update Invoice. InvoiceId: '{request.InvoiceId}'.", 500);
            }

            return BaseResponse<UpdateInvoiceRegisterCommandResponse>.Success(_mapper.Map<UpdateInvoiceRegisterCommandResponse>(updatedInvoice), 200);
        }
    }
}
