﻿using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Wallet;
using Infrastructure.DTOs.Response.Wallet;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<WalletService> logger;
        private readonly IMapper mapper;
        private readonly IStaffService staffService;

        public WalletService(IUnitOfWork unitOfWork, ILogger<WalletService> logger, IMapper mapper, IStaffService staffService)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.staffService = staffService;
        }

        public async Task<WalletRechargeBalanceResponse> RechargeBalanceWithCash(int walletId, WalletBalanceRechargeCashRequest cashRequest)
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
                decription: $"Customer recharge {cashRequest.Amount} VND by cash to wallet [Id: {wallet.Id}] balance");
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
                logger.LogError(ex, "Error occurred when trying to recharge money to wallet with Id: {walletId} with cash", wallet.Id);
                throw new Exception($"Error occurred when trying to recharge money to wallet with Id: {wallet.Id}");
            }
            finally
            {
                await unitOfWork.DisposeAsync();
            }
        }

        private async Task<Wallet> GetAndValidateWallet(int walletId)
        {
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
    }
}