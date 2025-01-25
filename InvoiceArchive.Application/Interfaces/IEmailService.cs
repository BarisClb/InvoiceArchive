namespace InvoiceArchive.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendInvoiceInformationEmail(string invoiceId);
    }
}
