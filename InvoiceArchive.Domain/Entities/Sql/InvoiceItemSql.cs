namespace InvoiceArchive.Domain.Entities.Sql
{
    public class InvoiceItemSql : BaseEntitySql
    {
        public int InvoiceItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public int UnitPrice { get; set; }

        // Relations
        public Guid InvoiceGuid { get; set; }
        public string InvoiceId { get; set; }
        public InvoiceSql Invoice { get; set; }
    }
}
