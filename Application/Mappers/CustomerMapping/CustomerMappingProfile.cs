using AutoMapper;
using Infrastructure.DTOs.Response.Customer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Utils;

namespace Application.Mappers.CustomerMapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerResponse>().ForMember(dest => dest.CreateDate,
                src => src.MapFrom(src => DateTimeHelper.FormatDateTimeToDatetime(src.CreateDate)))
                  .ForMember(dest => dest.UpdateDate,
                src => src.MapFrom(src => DateTimeHelper.FormatDateTimeToDatetime(src.UpdateDate)))
                     .ForMember(dest => dest.DateOfBirth,
                src => src.MapFrom(src => DateTimeHelper.FormatDateTimeToDatetime(src.DateOfBirth)))
                .ForMember(dest => dest.Status,
                src => src.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(src => src.Id));
            CreateMap<PagedList<Customer>, PagedList<CustomerResponse>>();
        }
    }

}
