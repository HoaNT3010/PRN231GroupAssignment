using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Data
{
    public interface IUnitOfWork
    {
        // Repositories
        public IStaffRepository StaffRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public ICardRepository CardRepository { get; }
        public IWalletRepository WalletRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IInvoiceDetailRepository InvoiceDetailRepository { get; }

        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        public ValueTask DisposeAsync();
    }
}
