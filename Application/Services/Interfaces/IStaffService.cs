using Domain.Entities;
using Infrastructure.DTOs.Request.Staff;
using Infrastructure.DTOs.Response.Staff;

namespace Application.Services.Interfaces
{
    public interface IStaffService
    {
        Task<StaffLoginResponse?> Login(StaffLoginRequest loginRequest);
        /// <summary>
        /// Get user based on Http request's Jwt token information
        /// </summary>
        /// <returns></returns>
        Task<Staff?> GetCurrentStaff();
    }
}
