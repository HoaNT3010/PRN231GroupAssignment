using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Response.Invoice;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
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
    }
}
