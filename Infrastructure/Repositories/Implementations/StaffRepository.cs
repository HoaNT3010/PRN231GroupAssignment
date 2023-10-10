using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Staff?> GetByUsername(string username)
        {
            return await dbSet.FirstOrDefaultAsync(s => s.Username.ToLower() == username.Trim().ToLower());
        }

        public async Task<Staff?> Login(string username, string password)
        {
            var staff = await GetByUsername(username);
            if (staff == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, staff.PasswordHash))
            {
                return null;
            }
            return staff;
        }
    }
}
