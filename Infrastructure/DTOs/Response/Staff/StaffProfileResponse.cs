using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Response.Staff
{
    public class StaffProfileResponse
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string DateOfBirth { get; set; }
        public required Gender Gender { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }

    }
}
