using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Domain.Enums;

namespace Infrastructure.Repositories.Implementations
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Wallet> AddnewWallet(int cardId)
        {
            var newWallet = new Wallet()
            {
                CardId = cardId,
                Balance = 0,
                CreateDate = DateTime.Now,
                Status = WalletStatus.Active
            };
            await AddAsync(newWallet);
            return newWallet;
        }
    }
}
