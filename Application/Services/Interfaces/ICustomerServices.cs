using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.DTOs.Request.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICustomerServices
    {
        public  Task<Customer> AddNewCustomer(CustomerRequest customer);
<<<<<<< Updated upstream
=======
        public Task<Invoice> GetInvoiceWithCustomerId(int customerId);
>>>>>>> Stashed changes
        public Task<Customer> GetCustomerByID(int id);
        public Task<PagedList<Customer>> GetAll(int pageSize, int pageNumber);
        public void UpdateCustomer(UpdateCustomerRequest customer);
        //public Task<Customer> GetById(int id);
        //public Task<Customer> Delete(int id);
        //public Task<Customer> Update(int id);

    }
}
