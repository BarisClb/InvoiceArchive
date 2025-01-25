namespace InvoiceArchive.Application.Settings
{
    public class MongoDbSettings
    {
        public string? DatabaseName { get; set; }
        public string? InvoiceCollectionName { get; set; }
        public string? ConnectionString { get; set; }
    }
}
