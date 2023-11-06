using AutoMapper;
using Infrastructure.DTOs.Request.Staff;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.Staff
{
    public class StaffMappingUpdate :Profile
    {
        public StaffMappingUpdate() {
            CreateMap<StaffUpdateRequest, Domain.Entities.Staff>()
                  .ForMember(dest => dest.DateOfBirth,
                  src => src.MapFrom(src => DateTimeHelper.ConvertDateStringToDateTime(src.DateOfBirth)))
                  .ForMember(dest => dest.PasswordHash,
                  src => src.MapFrom(src => BCrypt.Net.BCrypt.EnhancedHashPassword(src.Password)));
        }
    }
}
