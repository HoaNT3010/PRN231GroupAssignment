

using Domain.Enums;

namespace Infrastructure.DTOs.Request.Staff
{
    public class StaffUpdateRequest
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
    }
}
