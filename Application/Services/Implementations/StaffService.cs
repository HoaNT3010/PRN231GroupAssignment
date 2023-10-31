using Infrastructure.DTOs.Request.Staff;
using Infrastructure.Data;
using Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Infrastructure.DTOs.Response.Staff;
using Application.ErrorHandlers;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        public async Task CreateStaff(Staff newStaff)
        {

            if (newStaff != null)
            {
                var staff = unitOfWork.StaffRepository.GetById(newStaff.Id);
                if (staff != null)
                {
                    throw new NotFoundException("This ID had been used");
                }
                staff = unitOfWork.StaffRepository.GetByEmail(newStaff.Email);
                if (staff != null)
                {
                    throw new NotFoundException("This Email had been used");
                }
                await unitOfWork.StaffRepository.CreateStaff(newStaff);

            }
        }

        public async Task DeleteStaff(int id)
        {
            var staff = unitOfWork.StaffRepository.GetById(id);
            if (staff == null)
            {
                throw new NotFoundException("This ID doesn't exist");
            }
            await unitOfWork.StaffRepository.DeleteStaff(id);

        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            List<Staff> list = new List<Staff>();
            list = (List<Staff>)await unitOfWork.StaffRepository.GetAll();
            return list;
        }

        public async Task<IEnumerable<Staff>> GetAllByName(string name)
        {
            List<Staff> list = new List<Staff>();
            list = (List<Staff>)await unitOfWork.StaffRepository.GetAllByName(name);
            return list;
        }

        public async Task<Staff> GetByEmail(string email)
        {
            Staff s = null;
            if (email != null)
            {
                s = await unitOfWork.StaffRepository.GetByEmail(email);
            }
            return s;
        }

        public async Task<Staff> GetById(int id)
        {
            Staff s = null;
            if (id != null)
            {
                s = await unitOfWork.StaffRepository.GetById(id);
            }
            return s;
        }

        public async Task<Staff?> GetByUsername(string username)
        {
            Staff s = null;
            if (username != null)
            {
                s = await unitOfWork.StaffRepository.GetByUsername(username);
            }
            return s;
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

        public void UpdateStaff(Staff newStaff)
        {
            if (newStaff != null)
            {
                var staff = unitOfWork.StaffRepository.GetById(newStaff.Id);
                if (staff == null)
                {
                    throw new NotFoundException("This ID doesn't exist");
                }
                unitOfWork.StaffRepository.UpdateStaff(newStaff);
            }

        }
    }
}
