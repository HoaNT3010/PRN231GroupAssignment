﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Request.Product
{
    public class ProductUpdateRequest
    {
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? CategoryId { get; set; }
    }
}
