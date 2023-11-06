namespace Infrastructure.DTOs.Response.Momo
{
    public class MomoTransactionResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int WalletId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PayUrl { get; set; } = string.Empty;
        public string QrCodeUrl { get; set; } = string.Empty;
        public string Deeplink { get; set; } = string.Empty;
    }
}
