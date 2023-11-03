using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Common;
using Infrastructure.Common.Parameters;
using System.Linq.Expressions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories.Implementations
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Invoice>> GetInvoicesList(InvoiceListParameters parameters)
        {
            // Duration filter
            var durationFilter = CheckDurationFilter(parameters);

            // Status filter
            var statusFilter = CheckStatusFilter(parameters);

            // Combined filter
            var combinedFilter = GetCombinedFilterExpression(durationFilter, statusFilter);

            // Sort query
            var sortQuery = CheckSortOrderQuery(parameters);

            return await GetPaginatedAsync(parameters.PageSize, parameters.PageNumber, combinedFilter, sortQuery);
        }

        public async Task<Invoice?> GetInvoiceWithDetails(int id)
        {
            return await dbSet.Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        private Expression<Func<Invoice, bool>>? CheckDurationFilter(InvoiceListParameters parameters)
        {
            // Check for input durations
            if (parameters.FromDate == null || parameters.ToDate == null)
            {
                return null;
            }
            // Parse data
            var fromDate = DateTimeHelper.ConvertDateStringToDateTime(parameters.FromDate);
            var toDate = DateTimeHelper.ConvertDateStringToDateTime(parameters.ToDate);
            if (fromDate == null || toDate == null)
            {
                return null;
            }
            // Swap value
            if (DateTime.Compare((DateTime)fromDate!, (DateTime)toDate!) > 0)
            {
                (fromDate, toDate) = (toDate, fromDate);
            }
            return (i => i.CreateDate >= fromDate && i.CreateDate <= toDate);
        }
        private Expression<Func<Invoice, bool>>? CheckStatusFilter(InvoiceListParameters parameters)
        {
            if (parameters.Status == null)
            {
                return null;
            }
            return (i => i.Status == parameters.Status);
        }
        private Expression<Func<Invoice, bool>>? GetCombinedFilterExpression(Expression<Func<Invoice, bool>>? durationFilter, Expression<Func<Invoice, bool>>? statusFilter)
        {
            if (durationFilter == null && statusFilter == null)
            {
                return null;
            }
            Expression combinedExpression = null;
            ParameterExpression parameter = Expression.Parameter(typeof(Invoice));
            if (durationFilter != null)
            {
                combinedExpression = Expression.Invoke(durationFilter, parameter);
            }
            if (statusFilter != null)
            {
                combinedExpression = combinedExpression != null
                    ? Expression.AndAlso(combinedExpression, Expression.Invoke(statusFilter, parameter))
                    : Expression.Invoke(statusFilter, parameter);
            }
            return Expression.Lambda<Func<Invoice, bool>>(combinedExpression!, parameter);
        }
        private Func<IQueryable<Invoice>, IOrderedQueryable<Invoice>> CheckSortOrderQuery(InvoiceListParameters parameters)
        {
            switch (parameters.SortOrder)
            {
                case Domain.Enums.InvoiceSortOrder.TimeDescending:
                    return (query => query.OrderByDescending(i => i.CreateDate));
                case Domain.Enums.InvoiceSortOrder.TimeAscending:
                    return (query => query.OrderBy(i => i.CreateDate));
                case Domain.Enums.InvoiceSortOrder.TotalPriceDescending:
                    return (query => query.OrderByDescending(i => i.TotalPrice));
                case Domain.Enums.InvoiceSortOrder.TotalPriceAscending:
                    return (query => query.OrderBy(i => i.TotalPrice));
                default:
                    return (query => query.OrderByDescending(i => i.CreateDate));
            }
        }

        public async Task<Invoice?> GetInvoiceWithCustomerId(int customerId)
        {

            return await dbSet.Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.Customer.Id == customerId);
        }

    }
}
