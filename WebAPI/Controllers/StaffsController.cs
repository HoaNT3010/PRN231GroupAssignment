using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.DTOs.Request.Staff;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/staff")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        /// <summary>
        /// Get All Staff In Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _staffService.GetAll());
        }
        /// <summary>
        /// Get a Staff by his/her ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var staff = await _staffService.GetById(id);
                if (staff != null)
                {
                    return Ok(staff);
                }
                return NotFound();
            }
            catch { return BadRequest(); }
        }
        /// <summary>
        /// Get All Customer by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var staff = await _staffService.GetAllByName(name);
                if (staff != null)
                {
                    return Ok(staff);
                }
                return NotFound();
            }
            catch { return BadRequest(); }
        }
        /// <summary>
        /// Get a Staff by his/her email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var staff = await _staffService.GetByEmail(email);
                if (staff != null)
                {
                    return Ok(staff);
                }
                return NotFound();
            }
            catch { return BadRequest(); }
        }

        /// <summary>
        /// Create a Staff information
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Staff staff)
        {
            try
            {
                await _staffService.CreateStaff(staff);
                return Ok(new { Success = true, Data = "ID:" + staff.Id + " Email " + staff.Email });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = ex.Message });
            }
        }

        /// <summary>
        /// Delete a Staff from Data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _staffService.DeleteStaff(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = ex.Message });
            }
        }

        /// <summary>
        /// Update Staff information by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Staff staff)
        {
            try
            {
                if (id != staff.Id)
                {
                    return BadRequest();
                }
                if (await GetById(id) == null)
                {
                    return NotFound();
                }
                 _staffService.UpdateStaff(staff);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateStaff([FromBody] StaffCreateRequest createRequest)
        {
            return Ok();
        }
    }
}
    
