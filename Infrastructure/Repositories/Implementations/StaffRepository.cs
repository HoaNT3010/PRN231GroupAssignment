﻿using Domain.Entities;
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

        public async Task CreateStaff(Staff newStaff)
        {
             await AddAsync(newStaff);
                }

        public async Task DeleteStaff(int id)
        {
            Staff s = await GetByIdAsync(id);
            if (s != null) {
                s.Status = 0;
            }
                }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await GetAllAsync();        }

        public async Task<IEnumerable<Staff>> GetAllByName(string name)
        {
            List<Staff> list = new List<Staff>();
            List<Staff> listAll= (List<Staff>)await GetAllAsync();
            if(listAll.Count > 0)
            {
                foreach (Staff s in listAll)
                {
                    if (s.FirstName.ToLower().Contains(name.ToLower()) || s.LastName.ToLower().Contains(name.ToLower()))
                    {
                        list.Add(s);
                    }
                }
            }
            return list;
        }

        public Task<Staff> GetByEmail(string email)
        {
            return dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Staff> GetById(int id)
        {
            return await GetByIdAsync(id);
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

        public void UpdateStaff(Staff newStaff)
        {
           UpdateAsync(newStaff);
        }
    }
}
