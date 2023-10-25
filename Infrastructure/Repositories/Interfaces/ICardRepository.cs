using Domain.Entities;
using Infrastructure.DTOs.Request.Card;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        public Task<Card> AddNewCard(int CustomerId);

        Task<Card?> GetCardWithWallets(int id);
    }
}
