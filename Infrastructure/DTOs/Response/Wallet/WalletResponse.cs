namespace Infrastructure.DTOs.Response.Wallet
{
    public class WalletResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Balance { get; set; }
        public required string CreateDate { get; set; }
        public required string Status { get; set; }
        public bool IsDefaultWallet { get; set; }
    }
}
