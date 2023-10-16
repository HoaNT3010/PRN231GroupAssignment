using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<Invoice?> GetInvoiceWithDetails(int id);
    }
}
