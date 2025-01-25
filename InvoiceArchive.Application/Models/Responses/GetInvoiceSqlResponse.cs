namespace InvoiceArchive.Application.Models.Responses
{
    public class GetInvoiceSqlResponse
    {
        public virtual Guid Id { get; set; }
        public string InvoiceId { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverTitle { get; set; }
        public string Date { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }


        // Relations
        public ICollection<GetInvoiceItemSqlResponse>? InvoiceItems { get; set; }
    }

    public class GetInvoiceItemSqlResponse
    {
        public int InvoiceItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public int UnitPrice { get; set; }

        // Relations
        public Guid InvoiceGuid { get; set; }
        public string InvoiceId { get; set; }
    }

}
