namespace InvoiceArchive.Application.Responses
{
    public class SortedListRequest
    {
        public string? SearchWord { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool IsReversed { get; set; }
        public bool NeedCount { get; set; }
    }
}
