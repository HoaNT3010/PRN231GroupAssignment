using AutoMapper;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.DTOs.Response.InvoiceDetail;
using Infrastructure.Utils;

namespace Application.Mappers.Invoice
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            #region Invoice
            CreateMap<Domain.Entities.Invoice, InvoiceResponse>()
                .ForMember(dest => dest.CreateDate,
                src => src.MapFrom(src => DateTimeHelper.FormatDateTimeToDatetime(src.CreateDate)))
                .ForMember(dest => dest.Status,
                src => src.MapFrom(src => src.Status.ToString()));
            #endregion

            #region InvoiceDetail
            CreateMap<Domain.Entities.InvoiceDetail, InvoiceDetailResponse>();
            #endregion
        }
    }
}
