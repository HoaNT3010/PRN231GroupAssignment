using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<Invoice?> GetInvoiceWithDetails(int id);
        Task<PagedList<Invoice>> GetInvoicesList(InvoiceListParameters parameters);
    }
}
