using Domain.Entities;
using Infrastructure.DTOs.Request.Card;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICardRepository
    {
        public Task<Card> AddNewCard(int CustomerId);

    }
}
