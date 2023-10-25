using Infrastructure.DTOs.Request.Staff;
using Infrastructure.DTOs.Response.Staff;
using Application.ErrorHandlers;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Infrastructure.DTOs.Response;
using Domain.Enums;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Operations for system's authentication
    /// </summary>
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IStaffService staffService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IStaffService staffService)
        {
            this.logger = logger;
            this.staffService = staffService;
        }


        /// <summary>
        /// Log staff member into the system
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseObject<StaffLoginResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<ResponseObject<StaffLoginResponse>>> Login([FromBody] StaffLoginRequest loginRequest)
        {
            logger.LogInformation("Staff logging in into Store Sale System");
            var result = await staffService.Login(loginRequest);
            return Ok(new ResponseObject<StaffLoginResponse>()
            {
                Status = ResponseStatus.Success.ToString(),
                Message = "Staff successfully logged-in",
                Data = result,
            });
        }
    }
}
