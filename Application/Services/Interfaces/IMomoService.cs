using Infrastructure.DTOs.Request.Momo;
using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Momo;

namespace Application.Services.Interfaces
{
    public interface IMomoService
    {
        Task<MomoTransactionResponse?> CreateMomoPayment(int walletId, int staffId, WalletBalanceRechargeRequest rechargeRequest);
        (bool, string) ValidateMomoPaymentResult(MomoTransactionResultRequest result);
    }
}
