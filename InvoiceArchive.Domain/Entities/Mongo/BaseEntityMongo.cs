namespace InvoiceArchive.Domain.Entities.Mongo
{
    public class BaseEntityMongo
    {
        //[BsonId]
        //public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
