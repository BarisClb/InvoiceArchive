using InvoiceArchive.Application.Models.Enums;
using InvoiceArchive.Application.Responses;

namespace InvoiceArchive.Application.Models.Requests
{
    public class GetInvoiceListSqlRequest : SortedListRequest
    {
        public InvoiceSearchInType? SearchIn { get; set; }
        public InvoiceOrderByType? OrderBy { get; set; }
        public List<string>? Includes { get; set; }
    }
}
