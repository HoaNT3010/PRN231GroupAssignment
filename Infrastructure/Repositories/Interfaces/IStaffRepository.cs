using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<Staff?> Login(string username, string password);
        Task<Staff?> GetByUsername(string username);
        Task<IEnumerable<Staff>> GetAll();
        Task<Staff> GetById(int id);
        Task<IEnumerable<Staff>> GetAllByName(string name);
        Task<Staff> GetByEmail(string email);
        Task CreateStaff(Staff newStaff);
        void UpdateStaff(Staff newStaff);
        Task DeleteStaff(int id);
    }
}
