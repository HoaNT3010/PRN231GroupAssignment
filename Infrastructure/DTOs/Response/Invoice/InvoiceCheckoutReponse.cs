namespace Infrastructure.DTOs.Response.Invoice
{
    public class InvoiceCheckoutReponse
    {
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int InvoiceId { get; set; }
        public int CardId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; } 
    }
}
