using Domain.Entities;
using Infrastructure.DTOs.Request.Card;
using Infrastructure.DTOs.Request.Customer;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer> AddNewCustomer(CustomerRequest CustomerCreate);

        public Task<ICollection<Customer>> GetAll();

        public Task<Customer> GetCustomerByID(int id);
        Task<Customer> UpdateCustomer(int id);
        Task<Customer> DeleteCustomer(int id);
    }
}
