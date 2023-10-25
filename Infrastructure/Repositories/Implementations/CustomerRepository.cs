using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Customer;
using Domain.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System;
using Infrastructure.DTOs.Request.Card;
using Models.Constant;

namespace Infrastructure.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly ICardRepository cardRepository;
        private readonly IWalletRepository walletRepository;


        public CustomerRepository(StoreDbContext context, ICardRepository cardRepository, IWalletRepository walletRepository) : base(context)
        {
            this.cardRepository = cardRepository;
            this.walletRepository = walletRepository;
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
            if(newCustomer != null)
            {
             var newcard = await cardRepository.AddNewCard(newCustomer.Id);
                if (newcard != null)
                {
                    await walletRepository.AddnewWallet(newcard.Id);
                }
            }
            return newCustomer;
        }

        public async Task<ICollection<Customer>> GetAll()
        {
            //return await GetAllAsync(includeProperties: "Invoices,Cards", options: o=> o.OrderByDescending(x => x.Id).ToList());
            return await GetAllAsync();

        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var customer = await GetByIdAsync(x => x.Id == id, includeProperties:"Cards,Invoices");
            if (customer == null)
            {
                throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_FOUND);
            }
            return customer;
        }

        public async Task<Customer> UpdateCustomer(int id)
        {
            var currentCustomer = GetCustomerByID(id).Result;
            if (currentCustomer != null)
            {
                currentCustomer.Status = CustomerStatus.Active;
                await UpdateAsync(currentCustomer);
            }
            return currentCustomer;
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var currentCustomer = GetCustomerByID(id).Result;
            if (currentCustomer != null)
            {
              currentCustomer.Status = CustomerStatus.Inactive;
              await UpdateAsync(currentCustomer);
            }
            return  currentCustomer;
        }
    }
}
