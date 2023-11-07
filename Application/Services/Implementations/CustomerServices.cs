using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Customer;
using Infrastructure.DTOs.Response.Customer;
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
        private readonly IMapper mapper;
        public CustomerServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Customer> AddNewCustomer(CustomerRequest customer)
        {
            var newCustomer = unitOfWork.CustomerRepository.AddNewCustomer(customer).Result;
            await unitOfWork.SaveChangeAsync();

            if (newCustomer != null)
            {
                var newcard = await unitOfWork.CardRepository.AddDefaultCard(newCustomer.Id);
                await unitOfWork.SaveChangeAsync();
                if (newcard != null)
                {
                    await unitOfWork.WalletRepository.AddDefaultWallet(newcard.Id);
                    await unitOfWork.SaveChangeAsync();

                }
            }
            return newCustomer;

        }

        public async Task<PagedList<CustomerResponse>> GetAll(int pageSize, int pageNumber)
        {
            var paging = unitOfWork.CustomerRepository.GetAll(pageSize, pageNumber).Result;
            if (paging.Items.Count == 0)
            {
                pageSize = 10; pageNumber = 1;
                var result = await unitOfWork.CustomerRepository.GetAll(pageSize, pageNumber);
                return mapper.Map<PagedList<CustomerResponse>>(result);

            }
            return mapper.Map<PagedList<CustomerResponse>>(paging);


        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            return await unitOfWork.CustomerRepository.GetCustomerByID(id);
        }

        public async Task<Invoice> GetInvoiceWithCustomerId(int customerId)
        {
            return await unitOfWork.InvoiceRepository.GetInvoiceWithCustomerId(customerId);
        }

        public async void UpdateCustomer(UpdateCustomerRequest customer)
        {
           unitOfWork.CustomerRepository.UpdateCustomer(customer);
           await unitOfWork.SaveChangeAsync();
        }
    }
}
