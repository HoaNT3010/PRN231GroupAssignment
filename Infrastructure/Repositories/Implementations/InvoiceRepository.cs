using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
