using Application.ErrorHandlers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices customerService;

        public CustomerController(ICustomerServices customerService)
        {
 
            this.customerService = customerService;
        }


        [HttpGet]
        public async Task<PagedList<Customer>> GetAllCustomer(int pageSize, int pageNumber)
        {
            try
            {
               return await customerService.GetAll(pageSize, pageNumber);
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] CustomerRequest customer)
        {
            try
            {
                return Ok(await customerService.AddNewCustomer(customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutUpdateCustomer([FromBody] UpdateCustomerRequest customer)
        {
            try { 
            customerService.UpdateCustomer(customer);
            return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        }
    }
}
