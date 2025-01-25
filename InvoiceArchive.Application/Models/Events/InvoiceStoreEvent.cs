namespace InvoiceArchive.Application.Models.Events
{
    public class InvoiceStoreEvent : BaseIntegrationEvent
    {
        public string InvoiceId { get; set; }
        public bool IsStored { get; set; }
    }
}
