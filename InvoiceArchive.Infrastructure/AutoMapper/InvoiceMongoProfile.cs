using AutoMapper;
using InvoiceArchive.Application.Commands.CreateInvoiceRegister;
using InvoiceArchive.Application.Commands.UpdateInvoiceRegister;
using InvoiceArchive.Application.Queries.GetInvoiceByIdFromRegister;
using InvoiceArchive.Application.Queries.GetInvoiceHeadersFromRegister;
using InvoiceArchive.Application.Queries.GetInvoicesToStoreFromRegister;
using InvoiceArchive.Domain.Entities.Mongo;

namespace InvoiceArchive.Infrastructure.AutoMapper
{
    public class InvoiceMongoProfile : Profile
    {
        public InvoiceMongoProfile()
        {
            CreateMap<InvoiceMongo, CreateInvoiceRegisterCommandRequest>().ForMember(invoice => invoice.InvoiceHeader, options => options.MapFrom(invoice => invoice.InvoiceHeader))
                                                                          .ForMember(invoice => invoice.InvoiceLine, options => options.MapFrom(invoice => invoice.InvoiceLine))
                                                                          .ReverseMap();
            CreateMap<CreateInvoiceRegisterCommandRequest, CreateInvoiceRegisterCommandResponse>().ForMember(invoice => invoice.InvoiceHeader, options => options.MapFrom(invoice => invoice.InvoiceHeader))
                                                                                                  .ForMember(invoice => invoice.InvoiceLine, options => options.MapFrom(invoice => invoice.InvoiceLine))
                                                                                                  .ReverseMap();

            CreateMap<InvoiceMongo, UpdateInvoiceRegisterCommandResponse>().ReverseMap();

            CreateMap<InvoiceHeaderMongo, GetInvoiceHeadersFromRegisterQueryResponse>().ReverseMap();
            CreateMap<InvoiceMongo, GetInvoiceHeadersFromRegisterQueryResponse>().ConstructUsing((source, context) => context.Mapper.Map<GetInvoiceHeadersFromRegisterQueryResponse>(source.InvoiceHeader))
                                                                                 .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.InvoiceId))
                                                                                 .ForMember(dest => dest.SenderTitle, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.SenderTitle))
                                                                                 .ForMember(dest => dest.SenderTitle, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.SenderTitle))
                                                                                 .ForMember(dest => dest.ReceiverTitle, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.ReceiverTitle))
                                                                                 .ForMember(dest => dest.Date, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.Date))
                                                                                 .ReverseMap();

            CreateMap<InvoiceMongo, GetInvoiceByIdFromRegisterQueryResponse>().ReverseMap();
            CreateMap<InvoiceMongo, GetInvoicesToStoreFromRegisterQueryResponse>().ForMember(invoice => invoice.InvoiceHeader, options => options.MapFrom(invoice => invoice.InvoiceHeader))
                                                                                  .ForMember(invoice => invoice.InvoiceLine, options => options.MapFrom(invoice => invoice.InvoiceLine))
                                                                                  .ReverseMap();
        }
    }
}
