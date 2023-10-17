using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Common.Parameters
{
    public class InvoiceListParameters : QueryStringParameters
    {
        public InvoiceStatus? Status { get; set; }
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/([0-9]{4})$", ErrorMessage = "Date format must be dd/mm/yyyy")]
        public string? FromDate { get; set; }
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/([0-9]{4})$", ErrorMessage = "Date format must be dd/mm/yyyy")]
        public string? ToDate { get; set; }
        public InvoiceSortOrder? SortOrder { get; set; }
    }
}
