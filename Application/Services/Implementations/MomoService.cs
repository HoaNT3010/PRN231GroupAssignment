using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Momo;
using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Momo;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Services.Implementations
{
    public class MomoService : IMomoService
    {
        private readonly ILogger<MomoService> logger;
        private readonly MomoOptions momoOptions;

        public MomoService(ILogger<MomoService> logger, IOptions<MomoOptions> momoOptions)
        {
            this.logger = logger;
            this.momoOptions = momoOptions.Value;
        }

        public async Task<MomoTransactionResponse?> CreateMomoPayment(int walletId, WalletBalanceRechargeRequest rechargeRequest)
        {
            logger.LogInformation("Momo service: Creating momo transaction for customer's wallet balance recharge request");
            string orderId = Guid.NewGuid().ToString();
            string orderInfo = $"User recharge wallet balance with Momo eWallet - Amount: {rechargeRequest.Amount} VND";
            CreateMomoPaymentRequest paymentRequest = new CreateMomoPaymentRequest(momoOptions.PartnerCode,
                orderId,
                (long)rechargeRequest.Amount,
                orderId,
                orderInfo,
                momoOptions.ReturnUrl,
                momoOptions.IpnUrl,
                "captureWallet",
                "",
                "vi");
            paymentRequest.MakeSignature(momoOptions.AccessKey, momoOptions.SecretKey);
            (bool paymentResult, string? paymentData) = paymentRequest.GetPaymentMethod(momoOptions.PaymentUrl);
            if (!paymentResult)
            {
                return null;

            }
            var paymentResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponse>(paymentData!);
            return new MomoTransactionResponse()
            {
                Status = ResponseStatus.Success.ToString(),
                Description = "Successfully create Momo transaction for recharge customer's wallet balance. Please confirm and checkout the Momo transaction",
                Amount = paymentResponse!.amount,
                WalletId = walletId,
                PaymentMethod = TransactionMethod.eWallet.ToString(),
                PayUrl = paymentResponse.payUrl,
                QrCodeUrl = paymentResponse.qrCodeUrl,
                Deeplink = paymentResponse.deeplink,
            };
        }
    }
}
