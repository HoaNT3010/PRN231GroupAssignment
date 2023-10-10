using Application.DTOs.Request.Staff;
using Application.DTOs.Response.Staff;

namespace Application.Services.Interfaces
{
    public interface IStaffService
    {
        Task<StaffLoginResponse?> Login(StaffLoginRequest loginRequest);
    }
}
