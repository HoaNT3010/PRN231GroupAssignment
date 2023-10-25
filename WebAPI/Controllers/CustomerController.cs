using Application.ErrorHandlers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.DTOs.Request.Card;
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
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomer(int page = 1, int pageSize = 10)
        {
            try
            {
                var response = await customerService.GetAll();
                var totalCount = response.Count;
                var totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = response.Skip((page -1)*pageSize).Take(pageSize).ToList();
               return Ok(productsPerPage);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByID([FromRoute] int id)
        {
            var customer = await customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
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

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
               await customerService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<Customer>> Update(int id)
        {
            try
            {
                await customerService.Update(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
