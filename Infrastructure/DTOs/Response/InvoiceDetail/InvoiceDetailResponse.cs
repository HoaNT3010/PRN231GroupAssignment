namespace Infrastructure.DTOs.Response.InvoiceDetail
{
    public class InvoiceDetailResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ItemTotal { get; set; }
        public int ProductId { get; set; }
    }
}
