﻿using Application.ErrorHandlers;
using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.Common.Parameters;
using Infrastructure.DTOs.Response;
using Infrastructure.DTOs.Response.Invoice;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Operations related to invoices and invoice's details
    /// </summary>
    [Route("api/v1/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ILogger<InvoicesController> logger;
        private readonly IInvoiceService invoiceService;

        public InvoicesController(ILogger<InvoicesController> logger, IInvoiceService invoiceService)
        {
            this.logger = logger;
            this.invoiceService = invoiceService;
        }

        /// <summary>
        /// Get detail information of an invoice by invoice Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("history/{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<InvoiceResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<ResponseObject<InvoiceResponse>>> GetInvoiceById([FromRoute] int id)
        {
            logger.LogInformation("Getting data of invoice with Id: {id}", id);
            var result = await invoiceService.GetInvoiceById(id);

            return Ok(new ResponseObject<InvoiceResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = string.Empty,
                Data = result
            });
        }

        /// <summary>
        /// Get paginated list of invoices with filters (duration, status,...) and sorting
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("history")]
        [Produces("application/json")]
        public async Task<ActionResult> GetInvoiceList([FromQuery] InvoiceListParameters parameters)
        {
            return NoContent();
        }
    }
}
