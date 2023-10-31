using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Card?> GetCardWithWallets(int id)
        {
            return await dbSet.Include(c => c.Wallets)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
