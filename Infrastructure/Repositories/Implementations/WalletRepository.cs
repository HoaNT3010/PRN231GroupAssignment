using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
