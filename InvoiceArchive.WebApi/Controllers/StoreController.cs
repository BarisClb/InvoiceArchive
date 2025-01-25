using InvoiceArchive.Application.Interfaces;
using InvoiceArchive.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceArchive.WebApi.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IInvoiceSqlService _invoiceSqlService;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IInvoiceSqlService invoiceSqlService, ILogger<StoreController> logger)
        {
            _invoiceSqlService = invoiceSqlService;
            _logger = logger;
        }


        [HttpGet("addorupdatejobstoreinvoices")]
        public async Task<IActionResult> AddOrUpdateJobStoreInvoices(string cronExpression = "*/15 * * * *")
        {
            _logger.LogInformation($"AddOrUpdateJob: StoreInvoices Request received. CronTimer: {cronExpression}");
            Hangfire.RecurringJob.AddOrUpdate<IJobService>(job => job.StoreInvoices(), cronExpression);
            return Ok($"Successfully Added/Updated job: 'StoreInvoices'. CronTimer: {cronExpression}");
        }

        [HttpGet("getstoredinvoices")]
        public async Task<IActionResult> GetStoredInvoices()
        {
            _logger.LogInformation($"GetStoredInvoices Request received.");
            return Ok(await _invoiceSqlService.GetAllInvoices());
        }

        [HttpGet("getstoredinvoicebyid/{id}")]
        public async Task<IActionResult> GetStoredInvoiceById(Guid id)
        {
            _logger.LogInformation($"GetStoredInvoiceById Request received: {id}");
            return Ok(await _invoiceSqlService.GetInvoiceById(id));
        }

        [HttpGet("getstoredinvoicebyinvoiceid/{invoiceid}")]
        public async Task<IActionResult> GetStoredInvoiceByInvoiceId(string invoiceid)
        {
            _logger.LogInformation($"GetStoredInvoiceByInvoiceId Request received: {invoiceid}");
            return Ok(await _invoiceSqlService.GetInvoiceByInvoiceId(invoiceid));
        }

        [HttpGet("getstoredinvoiceslist")]
        public async Task<IActionResult> GetStoredInvoicesList([FromQuery] GetInvoiceListSqlRequest getInvoiceListRequest)
        {
            _logger.LogInformation($"GetStoredInvoicesList Request received: {getInvoiceListRequest}");
            return Ok(await _invoiceSqlService.GetInvoiceList(getInvoiceListRequest));
        }
    }
}
