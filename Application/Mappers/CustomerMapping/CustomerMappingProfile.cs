using AutoMapper;
using Infrastructure.DTOs.Response.Customer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Application.Mappers.CustomerMapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<PagedList<Customer>, PagedList<CustomerResponse>>();
        }
    }
}
