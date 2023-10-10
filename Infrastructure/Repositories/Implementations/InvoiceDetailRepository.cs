using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Implementations
{
    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
    {
        public InvoiceDetailRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
