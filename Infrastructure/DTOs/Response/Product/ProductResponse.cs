using Domain.Enums;
using Infrastructure.DTOs.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Response.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public ProductStatus? Status { get; set; }
        public CategoryResponse? Category { get; set; }
    }
}
