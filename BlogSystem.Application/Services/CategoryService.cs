using AutoMapper;
using BlogSystem.Application.DTOs.Categories;
using BlogSystem.Application.Features.Categories.Commands;
using BlogSystem.Application.Helpers;
using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository,IMapper mapper)
            {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Category> CreateAsync(string name, string? description, CancellationToken ct)
        {
            if ( await _repository.ExistsByNameAsync(name))
            {
                throw new Exception("Category name exists");
            }
            var slug =SlugHelper.GenerateSlug(name);
            var category = new Category
            {
                CategoryName = name,
                CategoryDescription = description,
                CateSlug = slug,
            };
            await _repository.AddAsync(category);
            return category;

        }

        public async Task<List<CategoryDto>> GetAllAsync(CancellationToken ct)
        {
            var listCate = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(listCate);
        }

        public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken ct)
        {
            var info =await _repository.GetByIdAsync(id);
            if(info == null)
            {
                throw new Exception("Category not found");
            }
            return _mapper.Map<CategoryDto>(info);
        }

        public async Task<bool> UpdateAsync(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var info = await _repository.GetByIdAsync((int)command.Id);
            if (info == null)
            { 
                throw new Exception("Category not found");
            }
            info.CategoryName = command.CategoryName;
            info.CateSlug= command.CateSlug;
            info.CategoryDescription = command.CategoryDescription;
            info.IsActive = command.IsActive;
            await _repository.UpdateAsync(info);
            return true;
        }
    }
}
