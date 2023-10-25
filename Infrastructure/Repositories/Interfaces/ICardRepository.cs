using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<Card?> GetCardWithWallets(int id);
    }
}
