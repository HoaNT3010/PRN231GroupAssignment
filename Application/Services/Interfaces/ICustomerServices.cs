using Domain.Entities;
using Infrastructure.DTOs.Request.Card;
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
        public Task<ICollection<Customer>> GetAll();
        public Task<Customer> GetById(int id);
        public Task<Customer> Delete(int id);
        public Task<Customer> Update(int id);

    }
}
