using AutoMapper;
using Infrastructure.DTOs.Response.Staff;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.Staff
{
    public class StaffMappingInformation : Profile
    {

        public StaffMappingInformation()
        {
            CreateMap<Domain.Entities.Staff, StaffProfileResponse>()
                    .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(src => src.DateOfBirth.ToString()))
                                   .ForMember(dest => dest.CreateDate, src => src.MapFrom(src => src.CreateDate.ToString()))
                    .ForMember(dest => dest.UpdateDate, src => src.MapFrom(src => src.UpdateDate.ToString()));
        }
    }
}
