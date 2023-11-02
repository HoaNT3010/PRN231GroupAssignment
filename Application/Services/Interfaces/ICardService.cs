using Infrastructure.DTOs.Response.Wallet;

namespace Application.Services.Interfaces
{
    public interface ICardService
    {
        Task<List<WalletResponse>> GetCardWallets(int cardId);
    }
}
