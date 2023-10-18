namespace Infrastructure.DTOs.Request.Invoice
{
    public class InvoiceCreateRequest
    {
        public List<InvoiceDetailCreateRequest> Products { get; set; } = new List<InvoiceDetailCreateRequest>();
    }
}
