using AutoMapper;
using Domain.Enums;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Category;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Request.Product;
using Infrastructure.DTOs.Response.Category;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.DTOs.Response.InvoiceDetail;
using Infrastructure.DTOs.Response.Product;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.Product
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Domain.Entities.Product, ProductResponse>()
                .ForMember(dest => dest.Category, src => src.MapFrom(src => new CategoryResponse
                {
                    Id = src.Category.Id,
                    Name = src.Category.Name,
                    Status = src.Category.Status
                }))

                .ForMember(dest => dest.Status,
                    src => src.MapFrom(src => src.Status.ToString()));

            CreateMap<ProductCreateRequest, Domain.Entities.Product>();

            CreateMap<PagedList<Domain.Entities.Product>, PagedList<ProductResponse>>();

            CreateMap<ProductUpdateRequest, Domain.Entities.Product>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProductStatusUpdateRequest, Domain.Entities.Product>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProductQuantityUpdateRequest, Domain.Entities.Product>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
