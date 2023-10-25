using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.DTOs.Request.Card;
using Domain.Enums;

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
        }
    }
}
