using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        public Task<Wallet> AddDefaultWallet(int customerId);

    }
}
