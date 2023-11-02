using AutoMapper;
using Infrastructure.DTOs.Response.Wallet;
using Infrastructure.Utils;

namespace Application.Mappers.Wallet
{
    public class WalletMappingProfile : Profile
    {
        public WalletMappingProfile()
        {
            CreateMap<Domain.Entities.Wallet, WalletResponse>()
                .ForMember(dest => dest.CreateDate,
                src => src.MapFrom(src => DateTimeHelper.FormatDateTimeToDatetime(src.CreateDate)))
                .ForMember(dest => dest.Status,
                src => src.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Name,
                src => src.NullSubstitute(string.Empty));
        }
    }
}
