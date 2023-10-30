using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Wallet> AddDefaultWallet(int cardId)
        {
            var newWallet = new Wallet()
            {
                CardId = cardId,
                Balance = 0,
                CreateDate = DateTime.Now,
                IsDefaultWallet = true,
                Status = WalletStatus.Active
            };
            await AddAsync(newWallet);
            return newWallet;
        }
    }
}
