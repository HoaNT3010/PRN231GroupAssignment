using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Momo;
using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Momo;
using Infrastructure.DTOs.Response.Wallet;
using Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<WalletService> logger;
        private readonly IMapper mapper;
        private readonly IStaffService staffService;
        private readonly IMomoService momoService;

        public WalletService(IUnitOfWork unitOfWork, ILogger<WalletService> logger, IMapper mapper, IStaffService staffService, IMomoService momoService)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.staffService = staffService;
            this.momoService = momoService;
        }

        public async Task<WalletRechargeBalanceResponse> RechargeBalanceWithCash(int walletId, WalletBalanceRechargeRequest cashRequest)
        {
            var currentStaff = await staffService.GetCurrentStaff();
            if (currentStaff == null)
            {
                throw new NotFoundException("Cannot find current staff data");
            }
            var wallet = await GetAndValidateWallet(walletId);
            var transaction = GenerateBalanceRechargeTransaction(wallet.Id,
                currentStaff.Id,
                cashRequest.Amount,
                TransactionMethod.Cash,
                decription: $"Customer recharge {cashRequest.Amount} VND to wallet balance by cash");
            try
            {
                await unitOfWork.BeginTransactionAsync();

                // Add transaction data
                await unitOfWork.TransactionRepository.AddAsync(transaction);
                // Update wallet's balance
                wallet.Balance += transaction.Amount;
                unitOfWork.WalletRepository.UpdateAsync(wallet);

                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();

                return new WalletRechargeBalanceResponse()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Description = $"Customer successfully recharge money to wallet's balance",
                    Amount = transaction.Amount,
                    WalletId = wallet.Id,
                    PaymentMethod = transaction.TransactionMethod.ToString(),
                };
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Error occurred when trying to process recharge money to wallet with Id: {walletId} by cash", wallet.Id);
                throw new Exception($"Error occurred when trying to process recharge money to wallet with Id: {wallet.Id} by cash");
            }
            finally
            {
                await unitOfWork.DisposeAsync();
            }
        }

        private async Task<Wallet> GetAndValidateWallet(int walletId)
        {
            if (walletId < 1)
            {
                throw new BadRequestException("Invalid wallet Id, must be larger than 0");
            }
            var wallet = await unitOfWork.WalletRepository.GetByIdAsync(walletId);
            if (wallet == null)
            {
                throw new NotFoundException($"Cannot find wallet with Id: {walletId}");
            }
            if (wallet.Status != Domain.Enums.WalletStatus.Active)
            {
                throw new BadRequestException($"Wallet is currently inactive. Only active wallet can be used to process transaction");
            }
            return wallet;
        }
        private Transaction GenerateBalanceRechargeTransaction(int walletId, int staffId, decimal amount, TransactionMethod transactionMethod, string? decription = null, string? eWalletTransaction = null)
        {
            return new Transaction()
            {
                Amount = amount,
                TransactionType = TransactionType.RechargeBalance,
                TransactionMethod = transactionMethod,
                Description = decription,
                EWalletTransaction = eWalletTransaction,
                CreateDate = DateTime.UtcNow,
                Status = TransactionStatus.Completed,
                WalletId = walletId,
                StaffId = staffId,
                InvoiceId = null,
            };
        }

        public async Task<MomoTransactionResponse?> RechargeBalanceWithMomo(int walletId, WalletBalanceRechargeRequest rechargeRequest)
        {
            var currentStaff = await staffService.GetCurrentStaff();
            if (currentStaff == null)
            {
                throw new NotFoundException("Cannot find current staff data");
            }
            var wallet = GetAndValidateWallet(walletId);
            return await momoService.CreateMomoPayment(walletId, currentStaff.Id, rechargeRequest);
        }

        public async Task ProcessMomoTransactionResult(MomoTransactionResultRequest result)
        {
            (bool validateTransaction, string message) = momoService.ValidateMomoPaymentResult(result);
            // Transaction is cancel or failed
            if (!validateTransaction)
            {
                return;
            }
            // Transaction is success
            var extraDataObject = JsonConvert.DeserializeObject<ExtraData>(HashHelper.DecodeFromBase64(result.extraData!));
            var wallet = await GetAndValidateWallet(extraDataObject!.WalletId);
            var transaction = GenerateBalanceRechargeTransaction(wallet.Id,
                extraDataObject.StaffId,
                result.amount,
                TransactionMethod.eWallet,
                $"Customer recharge {result.amount} VND to wallet balance by Momo e-wallet",
                result.transId);
            try
            {
                await unitOfWork.BeginTransactionAsync();

                await unitOfWork.TransactionRepository.AddAsync(transaction);
                wallet.Balance += transaction.Amount;
                unitOfWork.WalletRepository.UpdateAsync(wallet);

                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Error occurred when trying to process recharge money to wallet by with Id: {walletId} by momo e-wallet", wallet.Id);
                throw new Exception($"Error occurred when trying to process recharge money to wallet with Id: {wallet.Id} by momo e-wallet");
            }
            finally
            {
                await unitOfWork.DisposeAsync();
            }
        }
    }
}
