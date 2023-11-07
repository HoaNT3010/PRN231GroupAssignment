using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Parameters
{
    public class ProductListParameters : QueryStringParameters
    {
        public ProductSortOrder? SortProduct { get; set; }
    }
}
