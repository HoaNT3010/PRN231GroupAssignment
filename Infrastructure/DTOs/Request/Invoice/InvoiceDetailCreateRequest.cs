namespace Infrastructure.DTOs.Request.Invoice
{
    public class InvoiceDetailCreateRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
