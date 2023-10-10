using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
