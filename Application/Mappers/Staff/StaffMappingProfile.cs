using AutoMapper;
using Infrastructure.DTOs.Request.Staff;
using Infrastructure.Utils;

namespace Application.Mappers.Staff
{
    public class StaffMappingProfile : Profile
    {
        public StaffMappingProfile()
        {
            CreateMap<StaffCreateRequest, Domain.Entities.Staff>()
                .ForMember(dest => dest.DateOfBirth,
                src => src.MapFrom(src => DateTimeHelper.ConvertDateStringToDateTime(src.DateOfBirth)))
                .ForMember(dest => dest.PasswordHash,
                src => src.MapFrom(src => BCrypt.Net.BCrypt.EnhancedHashPassword(src.Password)));

        }
    }
}
