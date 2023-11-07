using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Request.Category
{
    public class CategoryStatusUpdateRequest
    {
        public CategoryStatus? Status { get; set; }
    }
}
