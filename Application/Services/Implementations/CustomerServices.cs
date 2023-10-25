using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Card;
using Infrastructure.DTOs.Request.Customer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IUnitOfWork unitOfWork;
        public CustomerServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Customer> AddNewCustomer(CustomerRequest customer)
        {
           return await unitOfWork.CustomerRepository.AddNewCustomer(customer);
           
        }

        public  async Task<Customer> Delete(int id)
        {
            return await  unitOfWork.CustomerRepository.DeleteCustomer(id);
        }

        public async Task<ICollection<Customer>> GetAll()
        {
           return await unitOfWork.CustomerRepository.GetAll(); 
        }

        public async Task<Customer> GetById(int id)
        {
            return await unitOfWork.CustomerRepository.GetCustomerByID(id);
        }

        public async Task<Customer> Update(int id)
        {
            return await unitOfWork.CustomerRepository.UpdateCustomer(id);

        }
    }
}
