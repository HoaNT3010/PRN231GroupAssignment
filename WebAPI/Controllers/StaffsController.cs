using Application.Mappers.Staff;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.DTOs.Request.Staff;
using Infrastructure.DTOs.Response;
using Infrastructure.DTOs.Response.Staff;
using Microsoft.AspNetCore.Authorization;
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
        [Route("api/v1/staff/GetAll")]
        [Authorize]
        public async Task<ActionResult<ResponseObject<List<StaffProfileResponse>>>> GetAll()
        {
            var list=await _staffService.GetAll();
           if (list!=null)
            {
                return Ok(new ResponseObject<List<StaffProfileResponse>>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Data = list
                }) ;
            }
            else
            {
                return Ok(new ResponseObject<StaffProfileResponse>()
                {
                    Status=ResponseStatus.Failed.ToString(),
                    Message="Empty"
                });
            }
        }
        /// <summary>
        /// Get a Staff by his/her ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("{id}")]
        [Route("api/v1/staff/Get/{Id}")]
        [Authorize]
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
        /// Create a Staff information
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/staff/Create")]
        [Authorize]
        public async Task<ActionResult<ResponseObject<StaffCreateRequest>>> Create([FromBody] StaffCreateRequest staff)
        {
            try
            {
               var result= await _staffService.CreateStaff(staff);
                return Ok(new ResponseObject<StaffCreateRequest>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = $"Create Staff Success",
                    Data = result
                }) ;
            }
            catch (Exception ex)
            {
                return Ok(new ResponseObject<StaffCreateRequest>() { 
                    Status=ResponseStatus.Failed.ToString(),
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a Staff from Data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Route("api/v1/staff/Delete/{id}")]
        [Authorize]
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
        [HttpPut]
        [Route("api/v1/staff/Update/{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseObject<StaffUpdateRequest>>> Update([FromBody] StaffUpdateRequest staff)
        {
            try
            {
                await _staffService.UpdateStaff(staff);
                return Ok(new ResponseObject<StaffUpdateRequest>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = $"Update succes",
                    Data = staff
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseObject<StaffUpdateRequest>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = ex.Message.ToString()
                });
              }

            }


        [HttpPost]
        [Route("api/vi/staff/Create")]
        public async Task<ActionResult<ResponseObject<StaffCreateRequest>>> CreateStaff([FromBody] StaffCreateRequest createRequest)
        {
            try
            {
                _staffService.CreateStaff(createRequest);
                return Ok(new ResponseObject<StaffCreateRequest>()
                {
                    Status= ResponseStatus.Success.ToString(),
                    Message="Create Success",
                    Data= createRequest
                });
            }catch(Exception ex)
            {
                return Ok(new ResponseObject<StaffCreateRequest>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = ex.Message.ToString()
                });
            }
        }
    }
}
    
