namespace Infrastructure.DTOs.Response.Wallet
{
    public class WalletRechargeBalanceResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int WalletId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
