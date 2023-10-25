using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Wallet;

namespace Application.Services.Interfaces
{
    public interface IWalletService
    {
        Task<WalletRechargeBalanceResponse> RechargeBalanceWithCash(int walletId, WalletBalanceRechargeCashRequest cashRequest);
    }
}
