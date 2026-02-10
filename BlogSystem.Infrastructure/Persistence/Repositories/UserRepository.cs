using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Interfaces;
using BlogSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task AssignDefaultRoleAsync(int userId)
        {
            var guestRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Editor");//lay role mac dinh de gan vao tai khoan
            if (guestRole != null)
            {
                //insert vao bang Userole
                var userRole = new UserRole
                {
                    UserId= userId,
                    RoleId= guestRole.RoleId,
                };
            await _context.UserRole.AddAsync(userRole);
            await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
           .Include(u => u.UserRole)
           .ThenInclude(ur => ur.Role)
           .FirstOrDefaultAsync(u => u.UserId == id && u.IsActive == 1);

        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                       .Include(u => u.UserRole)
                       .ThenInclude(ur => ur.Role)
                       .FirstOrDefaultAsync(u => u.Email == email && u.IsActive == 1);
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task<User> GetUserWithRoleAsync(int userId)
        {
            return await _context.Users
                    .Include(u => u.UserRole)
                    .ThenInclude(r => r.Role)
                    .FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive == 1);

        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
