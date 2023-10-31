using Application.DTOs.Request.Staff;
using Application.DTOs.Response.Staff;
using Domain.Entities;

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
        Task<Staff?> GetByUsername(string username);
        Task<IEnumerable<Staff>> GetAll();
        Task<Staff> GetById(int id);
        Task<IEnumerable<Staff>> GetAllByName(string name);
        Task<Staff> GetByEmail(string email);
        Task CreateStaff(Staff newStaff);
        void UpdateStaff(Staff newStaff);
        Task DeleteStaff(int id);
    }
}
