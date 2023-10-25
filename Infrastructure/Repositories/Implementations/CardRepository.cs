using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Card;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Card> AddNewCard(int CustomerId)
        {
            var newcard = new Card()
            {
                CreateDate = DateTime.Now,
                CustomerId = CustomerId,
                Status = CardStatus.Active,
            };
            await AddAsync(newcard);
            return newcard;
            
        public async Task<Card?> GetCardWithWallets(int id)
        {
            return await dbSet.Include(c => c.Wallets)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
