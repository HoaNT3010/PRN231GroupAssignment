using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Customer;
using Infrastructure.Repositories.Implementations;
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
            var newCustomer = unitOfWork.CustomerRepository.AddNewCustomer(customer).Result;
            if (newCustomer != null)
            {
                var newcard = await unitOfWork.CardRepository.AddDefaultCard(newCustomer.Id);
                if (newcard != null)
                {
                    await unitOfWork.WalletRepository.AddDefaultWallet(newcard.Id);
                    
                }
            }
            await unitOfWork.SaveChangeAsync();
            return newCustomer;

        }

        public async Task<PagedList<Customer>> GetAll(int pageSize, int pageNumber)
        {
            return await unitOfWork.CustomerRepository.GetAll(pageSize, pageNumber);
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            return await unitOfWork.CustomerRepository.GetCustomerByID(id);
        }

        public  void UpdateCustomer(UpdateCustomerRequest customer)
        {
            unitOfWork.CustomerRepository.UpdateCustomer(customer);
            unitOfWork.SaveChangeAsync();
        }
    }
}
