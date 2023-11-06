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
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTOs.Response;
using Domain.Enums;
using Application.Mappers.Staff;

namespace Application.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<StaffService> logger;
        private readonly IJwtService jwtService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;

        public StaffService(IUnitOfWork unitOfWork, ILogger<StaffService> logger, IJwtService jwtService, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.jwtService = jwtService;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
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

        public async Task<StaffCreateRequest> CreateStaff(StaffCreateRequest newStaff)
        {
            if (newStaff == null) throw new NotFoundException("Fill all information");
            if (newStaff != null)
            {
                var Staff = mapper.Map<Staff>(newStaff);
                Staff.Status =StaffStatus.Active;
                Staff.Role = StaffRole.Employee;
                Staff.CreateDate = DateTime.UtcNow;
                await unitOfWork.StaffRepository.CreateStaff(Staff);
            }
            return newStaff;
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

        public async Task<List<StaffProfileResponse>> GetAll()
        {
            List<Staff> listStaff = new List<Staff>();
            listStaff = (List<Staff>)await unitOfWork.StaffRepository.GetAllAsync();
            List<StaffProfileResponse> response = new List<StaffProfileResponse>();
            foreach (Staff profile in listStaff)
            {
                response.Add(mapper.Map<StaffProfileResponse>(profile));
            }
            return response;
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

        public async Task UpdateStaff(StaffUpdateRequest newStaff)
        {
            if (newStaff != null)
            {
                var staff = await unitOfWork.StaffRepository.GetById(newStaff.Id);
                if (staff == null)
                {
                    throw new NotFoundException("This ID doesn't exist");
                }
                var entityStaff = mapper.Map<Staff>(newStaff);
                entityStaff.UpdateDate = DateTime.UtcNow;
                entityStaff.CreateDate = staff.CreateDate;
                entityStaff.Invoices = staff.Invoices;
                entityStaff.Transactions = staff.Transactions;
                entityStaff.Role = staff.Role;
                entityStaff.Status = staff.Status;
                unitOfWork.StaffRepository.UpdateStaff(entityStaff);
            }

        }

        public async Task<List<StaffProfileResponse>> SearchStaff(string keyword, string type)
        {
            List<StaffProfileResponse> staffs = await GetAll();
            List<StaffProfileResponse> searchStaff = new List<StaffProfileResponse>();
            if (keyword == "")
            {
                return staffs;
            }
            else
            {
                switch (type)
                {
                    case "FirstName":
                        {
                            foreach (var staff in staffs)
                            {
                                if (staff.FirstName.ToUpper().Contains(keyword.ToUpper()))
                                {
                                    searchStaff.Add(staff);
                                }
                            }
                            break;
                        }
                    case "LastName":
                        {

                            foreach (var staff in staffs)
                            {
                                if (staff.LastName.ToUpper().Contains(keyword.ToUpper()))
                                {
                                    searchStaff.Add(staff);
                                }
                            }
                            break;
                        }
                    case "FullName":
                        {

                            foreach (var staff in staffs)
                            {
                                if (staff.FirstName.ToUpper().Contains(keyword.ToUpper()) || staff.LastName.ToUpper().Contains(keyword.ToUpper()))
                                {
                                    searchStaff.Add(staff);
                                }
                            }
                            break;
                        }
                    case "Email":
                        {

                            foreach (var staff in staffs)
                            {
                                if (staff.Email.ToUpper().Contains(keyword.ToUpper()))
                                {
                                    searchStaff.Add(staff);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            foreach (var staff in staffs)
                            {
                                if (staff.FirstName.ToUpper().Contains(keyword.ToUpper()) || staff.LastName.ToUpper().Contains(keyword.ToUpper()))
                                {
                                    searchStaff.Add(staff);
                                }
                            }
                            break;
                        }
                }
                return searchStaff;
            }


        }

        public async Task<Staff> UpdateRole(int id, bool upRole)
        {
            if (id != null)
            {
                var Staff = await GetById(id);
                if (Staff == null)
                {
                    throw new NotFoundException("This Staff ID doesn't exist");
                }
                if (upRole)
                {
                    Staff.Role = StaffRole.Manager;
                    unitOfWork.StaffRepository.UpdateStaff(Staff);
                }
                else
                {
                    Staff.Role = StaffRole.Employee;
                    unitOfWork.StaffRepository.UpdateStaff(Staff);
                }
                return Staff;
            }
            else
            {
                throw new NotFoundException("Please input ID");
            }
        }
    }
}
