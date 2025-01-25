using AutoMapper;
using InvoiceArchive.Application.Interfaces.Mongo;
using InvoiceArchive.Application.Queries.GetInvoiceByIdFromRegister;
using InvoiceArchive.Application.Responses;
using InvoiceArchive.Domain.Entities.Mongo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoiceArchive.Application.Commands.CreateInvoiceRegister
{
    public class CreateInvoiceRegisterCommandHandler : IRequestHandler<CreateInvoiceRegisterCommandRequest, BaseResponse<CreateInvoiceRegisterCommandResponse>>
    {
        private readonly IInvoiceMongoRepository _invoiceMongoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInvoiceRegisterCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateInvoiceRegisterCommandHandler(IInvoiceMongoRepository invoiceRepository, IMapper mapper, ILogger<CreateInvoiceRegisterCommandHandler> logger, IMediator mediator)
        {
            _invoiceMongoRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        public async Task<BaseResponse<CreateInvoiceRegisterCommandResponse>> Handle(CreateInvoiceRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            InvoiceMongo createInvoice = _mapper.Map<InvoiceMongo>(request);
            createInvoice.CreatedOn = DateTime.Now;

            var invoice = await _mediator.Send(new GetInvoiceByIdFromRegisterQueryRequest() { InvoiceId = request.InvoiceHeader.InvoiceId });

            if (invoice?.Data != null)
            {
                _logger.LogError($"CreateInvoiceRegister error, Invoice already exists. Id: '{request.InvoiceHeader.InvoiceId}'.");
                return BaseResponse<CreateInvoiceRegisterCommandResponse>.Fail($"CreateInvoiceRegister error, Invoice already exists. Id: '{request.InvoiceHeader.InvoiceId}'.", 400);
            }

            await _invoiceMongoRepository.InsertAsync(createInvoice);

            _logger.LogInformation($"Successfully registered and Invoice. Id: '{request.InvoiceHeader.InvoiceId}'.");

            return BaseResponse<CreateInvoiceRegisterCommandResponse>.Success(_mapper.Map<CreateInvoiceRegisterCommandResponse>(request), 201);
        }
    }
}
