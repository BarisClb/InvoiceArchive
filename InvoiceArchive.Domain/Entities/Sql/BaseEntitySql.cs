namespace InvoiceArchive.Domain.Entities.Sql
{
    public class BaseEntitySql
    {
        public virtual Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
