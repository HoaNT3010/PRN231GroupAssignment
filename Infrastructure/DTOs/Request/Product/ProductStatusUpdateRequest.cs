using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Request.Product
{
    public class ProductStatusUpdateRequest
    {
        public ProductStatus? Status { get; set; }
    }
}
