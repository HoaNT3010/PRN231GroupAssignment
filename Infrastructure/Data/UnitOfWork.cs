using Domain.Interfaces.Data;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        // Db context
        private readonly StoreDbContext context;

        // Db transaction
        private IDbContextTransaction? transaction;

        // Logger
        private readonly ILogger<UnitOfWork> logger;

        // Repositories
        public IStaffRepository StaffRepository { get; } = null!;
        public ICategoryRepository CategoryRepository { get; } = null!;
        public IProductRepository ProductRepository { get; } = null!;
        public ICustomerRepository CustomerRepository { get; } = null!;
        public ICardRepository CardRepository { get; } = null!;
        public IWalletRepository WalletRepository { get; } = null!;
        public ITransactionRepository TransactionRepository { get; } = null!;
        public IInvoiceRepository InvoiceRepository { get; } = null!;
        public IInvoiceDetailRepository InvoiceDetailRepository { get; } = null!;

        public UnitOfWork(StoreDbContext context, ILogger<UnitOfWork> logger, IStaffRepository staffRepository, ICategoryRepository categoryRepository, IProductRepository productRepository, ICustomerRepository customerRepository, ICardRepository cardRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository)
        {
            this.context = context;
            this.logger = logger;
            StaffRepository = staffRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            CustomerRepository = customerRepository;
            CardRepository = cardRepository;
            WalletRepository = walletRepository;
            TransactionRepository = transactionRepository;
            InvoiceRepository = invoiceRepository;
            InvoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task BeginTransactionAsync()
        {
            try
            {
                logger.LogInformation("Creating new database transaction\nTime: {Time}", DateTime.UtcNow);
                transaction = await context.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when starting new database transaction\nTime: {Time}", DateTime.UtcNow);
                throw new Exception("Error when starting new database transaction");
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                logger.LogInformation("Committing database transaction.\nTime: {Time}", DateTime.UtcNow);
                await transaction!.CommitAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when trying to commit database transaction\nTime: {Time}", DateTime.UtcNow);
                throw new Exception("Error when trying to commit database transaction");
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (transaction != null)
                {
                    await transaction.DisposeAsync();
                }
                await context.DisposeAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when trying to dispose UnitOfWork\nTime: {Time}", DateTime.UtcNow);
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                logger.LogWarning("Rollback database transaction.\nTime: {Time}", DateTime.UtcNow);
                await transaction!.RollbackAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when trying to rollback database transaction.\nTime: {Time}", DateTime.UtcNow);
                throw new Exception("Error when trying to rollback database transaction");
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            try
            {
                logger.LogInformation("Saving change(s) to database.\nTime: {Time}", DateTime.UtcNow);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when trying to save change(s) to database\nTime: {Time}", DateTime.UtcNow);
                throw new Exception("Error when trying to save change(s) to database");
            }
        }

        private bool disposed;
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
