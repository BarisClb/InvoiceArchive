namespace InvoiceArchive.Application.Models.Events
{
    public class BaseIntegrationEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }


        public BaseIntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public BaseIntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
    }
}
