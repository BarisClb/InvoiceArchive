using AutoMapper;
using InvoiceArchive.Application.Models.Responses;
using InvoiceArchive.Domain.Entities.Sql;

namespace InvoiceArchive.Infrastructure.AutoMapper
{
    public class InvoiceSqlProfile : Profile
    {
        public InvoiceSqlProfile()
        {
            CreateMap<InvoiceSql, GetInvoiceSqlResponse>().ReverseMap();
            CreateMap<InvoiceItemSql, GetInvoiceItemSqlResponse>().ReverseMap();
        }
    }
}
