using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Invoice?> GetInvoiceWithDetails(int id)
        {
            return await dbSet.Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
