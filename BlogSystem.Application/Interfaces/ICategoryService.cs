using BlogSystem.Application.DTOs.Categories;
using BlogSystem.Application.Features.Categories.Commands;
using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateAsync(
            string name,
            string? description,
            CancellationToken ct
            );
        Task<List<CategoryDto>> GetAllAsync(CancellationToken ct);
        Task<CategoryDto> GetByIdAsync(int id, CancellationToken ct);

        Task<bool> UpdateAsync(UpdateCategoryCommand command,CancellationToken cancellationToken);
    }
}
