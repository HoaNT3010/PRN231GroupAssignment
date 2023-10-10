using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
