using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Customer;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {

        public Task<Customer> AddNewCustomer(CustomerRequest CustomerCreate);
        public Task<PagedList<Customer>> GetAll(int pageSize, int pageNumber);
        public Task<Customer> GetCustomerByID(int id);
        public void UpdateCustomer(UpdateCustomerRequest customer);
    }
}
