using Infrastructure.DTOs.Request.Staff;
using Infrastructure.DTOs.Response.Staff;
using Domain.Entities;
using Infrastructure.DTOs.Response;
using Application.ErrorHandlers;
using Application.Services.Implementations;
using AutoMapper;
using Infrastructure.Data;
using System.Security.Claims;
using Domain.Enums;

namespace Application.Services.Interfaces
{
    public interface IStaffService
    {
        public Task<Staff?> GetCurrentStaff();

        public Task<StaffCreateRequest> CreateStaff(StaffCreateRequest newStaff);

        public Task DeleteStaff(int id);
        public Task<List<StaffProfileResponse>> GetAll();


        public Task<Staff> GetById(int id);

        public Task<Staff?> GetByUsername(string username);

        public Task<StaffLoginResponse?> Login(StaffLoginRequest loginRequest);
        public Task UpdateStaff(StaffUpdateRequest newStaff);
        public Task<List<StaffProfileResponse>> SearchStaff(string keyword, SearchType type);
        public Task<Staff> UpdateRole(int id, StaffRole upRole);
}
}
