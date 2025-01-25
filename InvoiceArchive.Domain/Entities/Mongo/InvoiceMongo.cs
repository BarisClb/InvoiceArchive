using MongoDB.Bson.Serialization.Attributes;

namespace InvoiceArchive.Domain.Entities.Mongo
{
    [BsonIgnoreExtraElements]
    public class InvoiceMongo : BaseEntityMongo
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public ICollection<InvoiceLineMongo> InvoiceLine { get; set; }
        public bool IsStored { get; set; }
    }

    public class InvoiceHeaderMongo
    {
        public string InvoiceId { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverTitle { get; set; }
        public string Date { get; set; }
    }

    public class InvoiceLineMongo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public int UnitPrice { get; set; }
    }
}
