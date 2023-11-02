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
        /// <param name="id">Id of invoice</param>
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
                Message = $"Successfully retrieve data of invoice with Id: {id}",
                Data = result
            });
        }

        /// <summary>
        /// Get paginated list of invoices with filters (duration, status,...) and sorting
        /// </summary>
        /// <param name="parameters">Parameters for filtering and sorting the invoice list</param>
        /// <returns></returns>
        [HttpGet]
        [Route("history")]
        [Authorize(Policy = IdentityData.ManagerPolicyName)]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<PagedList<InvoiceResponse>>))]
        public async Task<ActionResult<ResponseObject<PagedList<InvoiceResponse>>>> GetInvoiceList([FromQuery] InvoiceListParameters parameters)
        {
            logger.LogInformation("Retrieving data of invoices");
            var result = await invoiceService.GetInvoiceList(parameters);
            return Ok(new ResponseObject<PagedList<InvoiceResponse>>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = "Successfully retrieved paginated list of invoices",
                Data = result
            });
        }

        /// <summary>
        /// Create an invoice for customer purchasing order. Return a pending invoice for payment processing.
        /// </summary>
        /// <param name="request">Data for creating new invoice</param>
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
            return Ok(new ResponseObject<InvoiceResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Successfully create new pending invoice with Id: {result.Id}",
                Data = result
            });
        }

        /// <summary>
        /// Cancel a pending invoice (change status from pending to cancelled) when customer does not pay for the order.
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("cancel/{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<InvoiceResponse>))]
        public async Task<ActionResult<ResponseObject<InvoiceResponse>>> CancelInvoice([FromRoute] int id)
        {
            logger.LogInformation("Cancelling an invoice with Id: {id}", id);
            var result = await invoiceService.CancelInvoice(id);
            return Ok(new ResponseObject<InvoiceResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Invoice [Id: {id}] has been cancelled",
                Data = result
            });
        }

        /// <summary>
        /// Process customer's order invoice checkout with customer's card and default card's wallet.
        /// </summary>
        /// <param name="id">Id of order's invoice</param>
        /// <param name="request">Information of customer's card</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("checkout/{id}/default")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<InvoiceCheckoutReponse>))]
        public async Task<ActionResult<InvoiceCheckoutReponse>> CheckoutInvoiceDefaultWallet([FromRoute] int id, [FromBody] InvoiceCheckoutRequest request)
        {
            logger.LogInformation("Customer checkout with default card's wallet for invoice with Id: {id}", id);
            var result = await invoiceService.CheckoutInvoiceDefaultWallet(id, request);
            return Ok(new ResponseObject<InvoiceCheckoutReponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = $"Invoice [Id: {id}] has been checked-out",
                Data = result
            });
        }
    }
}
