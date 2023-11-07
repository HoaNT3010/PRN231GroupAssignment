using AutoMapper;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Response.Category;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.DTOs.Response.InvoiceDetail;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.Category
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Domain.Entities.Category, CategoryResponse>()
                .ForMember(dest => dest.Status,
                src => src.MapFrom(src => src.Status.ToString()));

            CreateMap<PagedList<Domain.Entities.Category>, PagedList<CategoryResponse>>();

            CreateMap<CategoryUpdateRequest, Domain.Entities.Category>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CategoryStatusUpdateRequest, Domain.Entities.Category>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
