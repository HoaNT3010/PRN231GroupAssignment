using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<InvoiceService> logger;
        private readonly IMapper mapper;
        private readonly IStaffService staffService;

        public InvoiceService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger, IMapper mapper, IStaffService staffService)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.staffService = staffService;
        }

        public async Task<InvoiceResponse> CreateInvoice(InvoiceCreateRequest request)
        {
            var currentStaff = await staffService.GetCurrentStaff();
            if (currentStaff == null)
            {
                throw new NotFoundException("Cannot find current staff data");
            }
            var invoiceDetails = await GenerateInvoiceDetails(request);
            var invoice = GenerateInvoice(invoiceDetails, currentStaff);
            try
            {
                await unitOfWork.BeginTransactionAsync();
                var invoiceEntity = await unitOfWork.InvoiceRepository.AddReturnEntityAsync(invoice);
                await unitOfWork.SaveChangeAsync();
                if (invoiceEntity == null)
                {
                    throw new Exception();
                }
                AssignInvoiceIdToDetail(invoiceDetails, invoiceEntity.Id);
                await unitOfWork.InvoiceDetailRepository.AddManyAsync(invoiceDetails);
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();
                return await GetInvoiceById(invoiceEntity.Id);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Error occurred when trying to create order invoice");
                throw new Exception("Error occurred when trying to create order invoice");
            }
        }

        public async Task<InvoiceResponse> GetInvoiceById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid invoice Id");
            }
            var invoice = await unitOfWork.InvoiceRepository.GetInvoiceWithDetails(id);
            if (invoice == null)
            {
                throw new NotFoundException($"Cannot find invoice with Id: {id}");
            }
            return mapper.Map<InvoiceResponse>(invoice);
        }

        public async Task<PagedList<InvoiceResponse>> GetInvoiceList(InvoiceListParameters parameters)
        {
            var invoiceList = await unitOfWork.InvoiceRepository.GetInvoicesList(parameters);
            if (invoiceList == null)
            {
                throw new NotFoundException("Cannot find invoices with given parameters");
            }
            return mapper.Map<PagedList<InvoiceResponse>>(invoiceList);
        }

        public async Task<InvoiceResponse> CancelInvoice(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid invoice Id");
            }
            var invoice = await unitOfWork.InvoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new NotFoundException($"Cannot find invoice with Id: {id}");
            }
            if (invoice.Status != Domain.Enums.InvoiceStatus.Pending)
            {
                throw new BadRequestException("Only pending invoice can be cancelled");
            }
            invoice.Status = Domain.Enums.InvoiceStatus.Cancelled;
            unitOfWork.InvoiceRepository.UpdateAsync(invoice);
            await unitOfWork.SaveChangeAsync();
            return mapper.Map<InvoiceResponse>(invoice);
        }

        public async Task<InvoiceCheckoutReponse> CheckoutInvoiceDefaultWallet(int id, InvoiceCheckoutRequest request)
        {
            var currentStaff = await staffService.GetCurrentStaff();
            if (currentStaff == null)
            {
                throw new NotFoundException("Cannot find current staff data");
            }
            // Retrieving and validate invoice
            if (id <= 0)
            {
                throw new BadRequestException("Invalid invoice Id");
            }
            var invoice = await unitOfWork.InvoiceRepository.GetInvoiceWithDetails(id);
            if (invoice == null)
            {
                throw new NotFoundException($"Cannot find invoice with Id: {id}");
            }
            if (invoice.Status != Domain.Enums.InvoiceStatus.Pending)
            {
                throw new BadRequestException("Only pending invoice can be checkout");
            }
            // Retrieving and validate customer's card and card's wallet
            var card = await GetCustomerCard(request.CustomerCardId);
            // Default wallet is the oldest wallet
            var defaultWallet = card!.Wallets.FirstOrDefault();
            if (defaultWallet!.Status != WalletStatus.Active)
            {
                throw new BadRequestException($"Card's default wallet is currently inactive. Only active wallet can be used to process transaction");
            }
            if (defaultWallet!.Balance < invoice.TotalPrice)
            {
                throw new BadRequestException($"Insufficient wallet's balance. " +
                    $"Current wallet balance: {defaultWallet.Balance} VND; " +
                    $"Required amount: {invoice.TotalPrice} VND; " +
                    $"Needed: {invoice.TotalPrice - defaultWallet.Balance} VND");
            }
            var transaction = GenerateInvoiceCheckoutTransaction(invoice, "Customer checkout order's invoice with default wallet", defaultWallet.Id, currentStaff.Id);
            try
            {
                await unitOfWork.BeginTransactionAsync();
                // Add transaction
                await unitOfWork.TransactionRepository.AddAsync(transaction);
                // Decrease wallet's balance
                defaultWallet.Balance -= invoice.TotalPrice;
                unitOfWork.WalletRepository.UpdateAsync(defaultWallet);
                // Update invoice
                invoice.Status = InvoiceStatus.Paid;
                invoice.CustomerId = card.CustomerId;
                unitOfWork.InvoiceRepository.UpdateAsync(invoice);
                // Decrease product quantity
                await DecreaseInvoiceProductQuantity(invoice.InvoiceDetails.ToList());
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();
                return new InvoiceCheckoutReponse()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Description = $"Customer successfully checkout invoice",
                    InvoiceId = invoice.Id,
                    CardId = request.CustomerCardId,
                    WalletId = defaultWallet.Id,
                    Amount = invoice.TotalPrice,
                };
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Error occurred when trying to process checkout invoice with Id: {invoiceId}", id);
                throw new Exception("Error occurred when trying to process checkout invoice");
            }
            finally
            {
                await unitOfWork.DisposeAsync();
            }
        }

        private async Task<List<InvoiceDetail>> GenerateInvoiceDetails(InvoiceCreateRequest request)
        {
            var invoiceDetails = mapper.Map<List<InvoiceDetail>>(request.Products);
            foreach (var detail in invoiceDetails)
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new NotFoundException($"Cannot find product with Id: {detail.ProductId}");
                }
                if (product.Quantity < detail.Quantity)
                {
                    throw new BadRequestException($"Insufficient quantity of product [Id: {product.Id}]. Current quantity: {product.Quantity}; Required quantity: {detail.Quantity}");
                }
                detail.UnitPrice = product.UnitPrice;
                detail.ItemTotal = detail.UnitPrice * detail.Quantity;
            }
            return invoiceDetails;
        }
        private Invoice GenerateInvoice(List<InvoiceDetail> invoiceDetails, Staff staff)
        {
            Invoice invoice = new Invoice()
            {
                CreateDate = DateTime.UtcNow,
                Status = Domain.Enums.InvoiceStatus.Pending,
                StaffId = staff.Id,
            };
            invoice.Description = $"Staff {staff.Username} create order for customer at {DateTimeHelper.FormatDateTimeToDatetime(invoice.CreateDate)}";
            foreach (var detail in invoiceDetails)
            {
                invoice.TotalPrice += detail.ItemTotal;
            }
            return invoice;
        }
        private void AssignInvoiceIdToDetail(List<InvoiceDetail> invoiceDetails, int invoiceId)
        {
            foreach (var detail in invoiceDetails)
            {
                detail.InvoiceId = invoiceId;
            }
        }
        private async Task<Card?> GetCustomerCard(int cardId)
        {
            var card = await unitOfWork.CardRepository.GetCardWithWallets(cardId);
            if (card == null)
            {
                throw new NotFoundException($"Cannot find customer's card with Id: {cardId}");
            }
            if (card.Status != CardStatus.Active)
            {
                throw new BadRequestException($"Customer's card is currently inactive. Only active card can be used to process transaction");
            }
            if (card.Wallets.IsNullOrEmpty())
            {
                throw new NotFoundException($"Card with Id: {cardId} does not contained any wallet");
            }
            card.Wallets.OrderBy(w => w.CreateDate);
            return card;
        }
        private Transaction GenerateInvoiceCheckoutTransaction(Invoice invoice, string? description, int walletId, int staffId)
        {
            return new Transaction()
            {
                Amount = invoice.TotalPrice,
                TransactionType = TransactionType.Purchase,
                TransactionMethod = TransactionMethod.Card,
                Description = description,
                EWalletTransaction = null,
                CreateDate = DateTime.UtcNow,
                Status = TransactionStatus.Completed,
                WalletId = walletId,
                StaffId = staffId,
                InvoiceId = invoice.Id,
            };
        }
        private async Task DecreaseInvoiceProductQuantity(List<InvoiceDetail> invoiceDetails)
        {
            foreach (var detail in invoiceDetails)
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new NotFoundException($"Cannot find product with Id: {detail.Id}");
                }
                if (product.Quantity < detail.Quantity)
                {
                    throw new BadRequestException($"Insufficient quantity of product [Id: {product.Id}]. Current quantity: {product.Quantity}; Required quantity: {detail.Quantity}");
                }
                product.Quantity -= detail.Quantity;
                unitOfWork.ProductRepository.UpdateAsync(product);
            }
        }
    }
}
