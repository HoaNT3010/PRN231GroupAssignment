using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        public Task<Wallet> AddnewWallet(int cardId);

    }
}
