using Infrastructure.DTOs.Request.Staff;
using Infrastructure.DTOs.Response.Staff;

namespace Application.Services.Interfaces
{
    public interface IStaffService
    {
        Task<StaffLoginResponse?> Login(StaffLoginRequest loginRequest);
    }
}
