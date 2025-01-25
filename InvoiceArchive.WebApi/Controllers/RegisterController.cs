using AutoMapper;
using InvoiceArchive.Application.Commands.CreateInvoiceRegister;
using InvoiceArchive.Application.Queries.GetInvoiceByIdFromRegister;
using InvoiceArchive.Application.Queries.GetInvoiceHeadersFromRegister;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceArchive.WebApi.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(IMediator mediator, IMapper mapper, ILogger<RegisterController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost("createinvoice")]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceRegisterCommandRequest createInvoiceRequest)
        {
            _logger.LogInformation($"CreateInvoice Request received: {createInvoiceRequest}");
            return Ok(await _mediator.Send(createInvoiceRequest));
        }

        [HttpGet("getinvoices")]
        public async Task<IActionResult> GetInvoices()
        {
            _logger.LogInformation($"CreateInvoiceRequest received.");
            return Ok(await _mediator.Send(new GetInvoiceHeadersFromRegisterQueryRequest()));
        }

        [HttpGet("getinvoicebyid/{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            _logger.LogInformation($"GetInvoiceById Request received: '{id}'.");
            return Ok(await _mediator.Send(new GetInvoiceByIdFromRegisterQueryRequest() { InvoiceId = id }));
        }
    }
}
