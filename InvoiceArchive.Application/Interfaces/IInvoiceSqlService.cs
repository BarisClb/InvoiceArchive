using InvoiceArchive.Application.Models.Requests;
using InvoiceArchive.Application.Models.Responses;
using InvoiceArchive.Application.Queries.GetInvoicesToStoreFromRegister;
using InvoiceArchive.Application.Responses;

namespace InvoiceArchive.Application.Interfaces
{
    public interface IInvoiceSqlService
    {
        Task<BaseResponse<List<GetInvoiceSqlResponse>>> GetAllInvoices();
        Task<BaseResponse<GetInvoiceSqlResponse>> GetInvoiceById(Guid id);
        Task<BaseResponse<GetInvoiceSqlResponse>> GetInvoiceByInvoiceId(string invoiceId);
        Task<BaseResponse<List<GetInvoiceSqlResponse>>> GetInvoiceList(GetInvoiceListSqlRequest getInvoiceListRequest);
        Task<bool> StoreInvoice(GetInvoicesToStoreFromRegisterQueryResponse invoiceMongo);
    }
}
