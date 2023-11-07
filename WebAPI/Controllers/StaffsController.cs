using Application.Mappers.Staff;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
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
        [Route("all")]
        //[Authorize]
        public async Task<ActionResult<ResponseObject<List<StaffProfileResponse>>>> GetAll()
        {
            var list = await _staffService.GetAll();
            if (list != null)
            {
                return Ok(new ResponseObject<List<StaffProfileResponse>>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Data = list
                });
            }
            else
            {
                return Ok(new ResponseObject<StaffProfileResponse>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = "Empty"
                });
            }
        }
        /// <summary>
        /// Get a Staff by his/her ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        public async Task<ActionResult<ResponseObject<StaffProfileResponse>>> GetById(int id)
        {
            try
            {
                var staff = await _staffService.GetById(id);
                if (staff != null)
                {
                    return Ok(new ResponseObject<StaffProfileResponse>()
                    {
                        Status = ResponseStatus.Success.ToString(),
                        Message = $"Get Staff by ID",
                        Data = staff
                    });
                }
                return Ok(new ResponseObject<StaffProfileResponse>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = $"Not found this Staff ID",

                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseObject<string>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = $"Get Staff by ID",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a Staff information
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        //[Authorize]
        public async Task<ActionResult<ResponseObject<StaffCreateRequest>>> Create([FromBody] StaffCreateRequest staff)
        {
            try
            {
                var result = await _staffService.CreateStaff(staff);
                return Ok(new ResponseObject<StaffCreateRequest>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = $"Create Staff Success",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseObject<StaffCreateRequest>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a Staff from Data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        //[Authorize]
        public async Task<ActionResult<ResponseObject<StaffProfileResponse>>> Delete(int id)
        {
            try
            {
                var s= await _staffService.GetById(id);
                if (s != null)
                {
                    await _staffService.DeleteStaff(id);
                    return Ok(new ResponseObject<StaffProfileResponse>()
                    {
                        Status = ResponseStatus.Success.ToString(),
                        Message = $"Delete Successfully",
                        Data=s
                    });
                }
                else
                {
                    return Ok(new ResponseObject<StaffProfileResponse>()
                    {
                        Status = ResponseStatus.Failed.ToString(),
                        Message = $"Delete Faile - Can't Find this Staff"
                    });
                }

            }
            catch (Exception ex)
            {
                return Ok(new ResponseObject<Staff>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Update Staff information by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        //[Authorize]
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
        [HttpPut]
        [Route("UpdateRole/{id}/{role}")]
        //[Authorize]
        public async Task<ActionResult<ResponseObject<Staff>>> UpdateRole([FromRoute] int id,[FromRoute] StaffRole role)
        {
            try
            {
                Staff staff= await _staffService.UpdateRole(id, role);
                return Ok(new ResponseObject<Staff>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Message = $"Update succes",
                    Data = staff
                });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseObject<Staff>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = ex.Message.ToString()
                });
            }

        }
        [HttpGet]
        [Route("Search/{keyword}/{type}")]
        //[Authorize]
        public async Task<ActionResult<ResponseObject<List<StaffProfileResponse>>>> Search([FromRoute] string keyword,[FromRoute] SearchType type)
        {
            try
            {
                List<StaffProfileResponse> s = await _staffService.SearchStaff(keyword, type);
                return Ok(new ResponseObject<List<StaffProfileResponse>>()
                {
                    Status = ResponseStatus.Success.ToString(),
                    Data = s
                });
            }catch(Exception ex)
            {
                return Ok(new ResponseObject<List<Staff>>()
                {
                    Status = ResponseStatus.Failed.ToString(),
                    Message = ex.Message.ToString()
                });
            }
        }
    }
}

