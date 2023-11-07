using Domain.Enums;


namespace Infrastructure.DTOs.Response.Customer
{
    public class CustomerResponse
    {

        public int CustomerId { get; }
        public CustomerStatus Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }


        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}