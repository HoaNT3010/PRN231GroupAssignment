using Infrastructure.DTOs.Response.Invoice;

namespace Application.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetInvoiceById(int id);
    }
}
