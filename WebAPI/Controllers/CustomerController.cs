using Application.ErrorHandlers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Entities;
<<<<<<< Updated upstream
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Customer;
=======
using Domain.Enums;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Customer;
using Infrastructure.DTOs.Response;
using Infrastructure.DTOs.Response.Customer;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    /// <summary>
    /// </summary>
<<<<<<< Updated upstream
    [Route("api/[controller]")]
=======
    [Route("api/v1/customer")]
>>>>>>> Stashed changes
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices customerService;

        public CustomerController(ICustomerServices customerService)
        {
 
            this.customerService = customerService;
        }
<<<<<<< Updated upstream


        [HttpGet]
        public async Task<PagedList<Customer>> GetAllCustomer(int pageSize, int pageNumber)
        {
            try
            {
               return await customerService.GetAll(pageSize, pageNumber);
=======
        /// <summary>
        /// Get paginated list of Customer
        /// </summary>
        /// <param name="pageSize">Parameters for page Size</param>
        /// <param name="pageNumber">Parameters for page Number</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<PagedList<Customer>>))]
        [HttpGet]
        public async Task<ActionResult> GetAllCustomer(int pageSize, int pageNumber)
        {
            try
            {
               var result = await customerService.GetAll(pageSize, pageNumber);
                return Ok(new ResponseObject<PagedList<Customer>>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = "Successfully retrieved paginated list of Customer",
                    Data = result
                });;
>>>>>>> Stashed changes
            }
            catch
            {
                return null;
            }
        }
<<<<<<< Updated upstream
=======
        /// <summary>
        /// Add new Customer
        /// </summary>
        /// <param name="customer">Parameters for Customer information</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<Customer>))]
        /// 
>>>>>>> Stashed changes
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] CustomerRequest customer)
        {
            try
            {
<<<<<<< Updated upstream
                return Ok(await customerService.AddNewCustomer(customer));
=======
                var result = await customerService.AddNewCustomer(customer);
                return Ok(new ResponseObject<Customer>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = "Successfully add new Customer",
                    Data = result
                }); ;
>>>>>>> Stashed changes
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
<<<<<<< Updated upstream
=======
        /// <summary>
        /// Update current Customer
        /// </summary>
        /// <param name="customer">Parameters for Customer information need update</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<Customer>))]
>>>>>>> Stashed changes
        [HttpPut]
        public async Task<IActionResult> PutUpdateCustomer([FromBody] UpdateCustomerRequest customer)
        {
            try { 
            customerService.UpdateCustomer(customer);
<<<<<<< Updated upstream
            return Ok();
=======
                return Ok(new ResponseObject<Customer>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = "Successfully update Customer",
                }); ;
>>>>>>> Stashed changes
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
<<<<<<< Updated upstream
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByID([FromRoute] int id)
        {
            var customer = await customerService.GetCustomerByID(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
=======
        }
        /// <summary>
        /// Search Curtomer By Id
        /// </summary>
        /// <param name="id">Parameters id  to search Customer</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<Customer>))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByID([FromRoute] int id)
        {
            try
            {
                var customer = await customerService.GetCustomerByID(id);

                return Ok(new ResponseObject<Customer>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = "Successfully update Customer",
                    Data = customer
                }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Search Invoice with customerId
        /// </summary>
        /// <param name="id">Parameters id  to search Invoice with  customerId</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<Invoice>))]
        [HttpGet("InvoiceCustomer/{customerId}")]
        public async Task<ActionResult<Invoice>> GetInvoinceCustomer([FromRoute] int customerId)
        {
            try
            {
                var customer = await customerService.GetInvoiceWithCustomerId(customerId);

                return Ok(new ResponseObject<Invoice>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = "Successfully update Customer",
                    Data = customer
                }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

>>>>>>> Stashed changes
        }
    }
}
