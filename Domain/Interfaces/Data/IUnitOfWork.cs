namespace Domain.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        public ValueTask DisposeAsync();
    }
}
