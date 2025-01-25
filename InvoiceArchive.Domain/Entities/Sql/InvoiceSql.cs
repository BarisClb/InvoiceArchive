namespace InvoiceArchive.Domain.Entities.Sql
{
    public class InvoiceSql : BaseEntitySql
    {
        public string InvoiceId { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverTitle { get; set; }
        public string Date { get; set; }

        // Relations
        public ICollection<InvoiceItemSql>? InvoiceItems { get; set; }
    }
}
