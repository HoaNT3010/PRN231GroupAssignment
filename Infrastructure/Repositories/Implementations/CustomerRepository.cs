using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Customer;
using Infrastructure.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(StoreDbContext context) : base(context)
        {
            
        }

        public async Task<Customer> AddNewCustomer(CustomerRequest CustomerCreate)
        {
            var newCustomer = new Customer()
            {
                FirstName = CustomerCreate.FirstName,
                LastName = CustomerCreate.LastName,
                PhoneNumber = CustomerCreate.PhoneNumber,
                DateOfBirth = CustomerCreate.DateOfBirth,
                Gender = CustomerCreate.Gender,
                Email = CustomerCreate.Email,
                Address = CustomerCreate.Address,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Status = CustomerStatus.Active,
            };
            await AddAsync(newCustomer);
            return newCustomer;
        }

        public async Task<PagedList<Customer>> GetAll(int pageSize, int pageNumber)
        {
            return await GetPaginatedAsync(pageSize, pageNumber);
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            return await dbSet.Include(x => x.Cards).Include(x => x.Invoices).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateCustomer(UpdateCustomerRequest customer)
        {
            var currentCustomer = GetCustomerByID(customer.Id).Result;
            currentCustomer.Status = customer.Status;
            currentCustomer.UpdateDate = DateTime.Now;
       
        }
    }
}
