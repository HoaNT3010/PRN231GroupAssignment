using Infrastructure.DTOs.Response.InvoiceDetail;

namespace Infrastructure.DTOs.Response.Invoice
{
    public class InvoiceResponse
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string? CreateDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public List<InvoiceDetailResponse>? InvoiceDetails { get; set; }
    }
}
