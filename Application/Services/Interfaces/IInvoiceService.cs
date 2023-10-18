using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using Infrastructure.DTOs.Request.Invoice;
using Infrastructure.DTOs.Response.Invoice;

namespace Application.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetInvoiceById(int id);
        Task<PagedList<InvoiceResponse>> GetInvoiceList(InvoiceListParameters parameters);
        Task<InvoiceResponse> CreateInvoice(InvoiceCreateRequest request);
        Task<InvoiceResponse> CancelInvoice(int id);
    }
}
