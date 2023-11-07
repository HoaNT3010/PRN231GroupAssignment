using Domain.Enums;


namespace Infrastructure.DTOs.Response.Customer
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public CustomerStatus Status { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? CreateDate { get; set; }
        public string? UpdateDate { get; set; }
    }
}