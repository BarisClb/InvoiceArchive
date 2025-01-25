using InvoiceArchive.Application.Commands.UpdateInvoiceRegister;
using InvoiceArchive.Application.Models.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoiceArchive.Application.Consumers
{
    public class InvoiceStoreEventConsumer : IConsumer<InvoiceStoreEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvoiceStoreEventConsumer> _logger;

        public InvoiceStoreEventConsumer(IMediator mediator, ILogger<InvoiceStoreEventConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<InvoiceStoreEvent> context)
        {
            UpdateInvoiceRegisterCommandRequest request = new()
            {
                InvoiceId = context.Message.InvoiceId,
                IsStored = context.Message.IsStored,
            };

            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
            {
                _logger.LogError($"InvoiceStoreEvent Failed to Consume. Invoice Id : '{context.Message.InvoiceId}'. Result: {result}.");
            }
            _logger.LogInformation($"InvoiceStoreEvent Successfully Consumed. Invoice Id : '{context.Message.InvoiceId}'. Result: {result}.");
        }
    }
}
