using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Task<Staff?> Login(string username, string password);
        Task<Staff?> GetByUsername(string username);
    }
}
