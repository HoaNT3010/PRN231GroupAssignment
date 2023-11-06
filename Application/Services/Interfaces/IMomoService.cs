using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Momo;

namespace Application.Services.Interfaces
{
    public interface IMomoService
    {
        Task<MomoTransactionResponse?> CreateMomoPayment(int walletId, WalletBalanceRechargeRequest rechargeRequest);
    }
}
