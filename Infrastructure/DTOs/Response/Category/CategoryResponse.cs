using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Response.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CategoryStatus? Status { get; set; }
    }
}
