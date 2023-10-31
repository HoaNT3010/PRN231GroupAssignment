using Infrastructure.DTOs.Request.Staff;
using Infrastructure.Data;
using Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Infrastructure.DTOs.Response.Staff;
using Application.ErrorHandlers;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<StaffService> logger;
        private readonly IJwtService jwtService;
        private readonly IHttpContextAccessor contextAccessor;

        public StaffService(IUnitOfWork unitOfWork, ILogger<StaffService> logger, IJwtService jwtService, IHttpContextAccessor contextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.jwtService = jwtService;
            this.contextAccessor = contextAccessor;
        }

        public async Task<Staff?> GetCurrentStaff()
        {
            var identity = contextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }
            bool staffIdResult = int.TryParse(identity.FindFirst("Id")?.Value, out int staffId);
            return (!staffIdResult) ? null : await unitOfWork.StaffRepository.GetByIdAsync(staffId);
        }

        public async Task<StaffLoginResponse?> Login(StaffLoginRequest loginRequest)
        {

            var staff = await unitOfWork.StaffRepository.Login(loginRequest.Username, loginRequest.Password);
            if (staff == null)
            {
                throw new NotFoundException("Incorrect username or password");
            }
            StaffLoginResponse result = new() { AccessToken = jwtService.GenerateAccessToken(staff) };
            return result;
        }
    }
}
