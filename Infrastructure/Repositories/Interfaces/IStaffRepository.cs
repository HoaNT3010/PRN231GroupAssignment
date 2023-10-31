using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<Staff?> Login(string username, string password);
        Task<Staff?> GetByUsername(string username);
    }
}
