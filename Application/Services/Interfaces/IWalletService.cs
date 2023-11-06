using Infrastructure.DTOs.Request.Momo;
using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Momo;
using Infrastructure.DTOs.Response.Wallet;

namespace Application.Services.Interfaces
{
    public interface IWalletService
    {
        Task<WalletRechargeBalanceResponse> RechargeBalanceWithCash(int walletId, WalletBalanceRechargeRequest cashRequest);
        Task<MomoTransactionResponse?> RechargeBalanceWithMomo(int walletId, WalletBalanceRechargeRequest rechargeRequest);
        Task ProcessMomoTransactionResult(MomoTransactionResultRequest result);
    }
}
