using Application.ErrorHandlers;
using Application.Services.Interfaces;
using Domain.Enums;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Response;
using Infrastructure.DTOs.Response.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.OptionsSetup;

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
            logger.LogInformation("Retrieving data of invoice with Id: {id}", id);
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
        [Authorize(Policy = IdentityData.ManagerPolicyName)]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<PagedList<InvoiceResponse>>))]
        public async Task<ActionResult> GetInvoiceList([FromQuery] InvoiceListParameters parameters)
        {
            logger.LogInformation("Retrieving data of invoices");
            var result = await invoiceService.GetInvoiceList(parameters);
            return Ok(new ResponseObject<PagedList<InvoiceResponse>>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = string.Empty,
                Data = result
            });
        }

        /// <summary>
        /// Create an invoice for customer purchasing order. Return a pending invoice for payment processing.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<InvoiceResponse>))]
        public async Task<ActionResult<ResponseObject<InvoiceResponse>>> CreateInvoice([FromBody] InvoiceCreateRequest request)
        {
            logger.LogInformation("Creating order invoice with {itemCount} item(s)", request.Products.Count);
            var result = await invoiceService.CreateInvoice(request);
            return Ok(result);
        }
    }
}
