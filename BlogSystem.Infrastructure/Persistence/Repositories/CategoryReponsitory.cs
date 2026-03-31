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
    public class CategoryReponsitory : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryReponsitory(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryName == name && c.IsActive == 1);
        }

        public  async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Where(c => c.IsActive == 1).OrderByDescending(c=>c.CreatedAt).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                                 .FirstOrDefaultAsync(c => c.CategoryId == id && c.IsActive == 1);
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
