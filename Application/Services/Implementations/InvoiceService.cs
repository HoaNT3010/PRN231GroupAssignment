﻿using Application.ErrorHandlers;
using Application.Services.Interfaces;
using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.Data;
using Infrastructure.DTOs.Response.Invoice;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<InvoiceService> logger;
        private readonly IMapper mapper;

        public InvoiceService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
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
                throw new NotFoundException("Cannot find invoice with Id");
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
    }
}
