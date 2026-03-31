using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Interfaces
{
    public  interface ICategoryRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task AddAsync(Category category);
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task UpdateAsync(Category category);

    }
}
