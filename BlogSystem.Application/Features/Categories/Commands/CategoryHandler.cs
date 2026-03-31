using AutoMapper;
using BlogSystem.Application.DTOs.Categories;
using BlogSystem.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Commands
{
    public class CategoryHandler : IRequestHandler<CategoryCommands,CategoryDto>
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public CategoryHandler(ICategoryService sevice,IMapper mapper )
        {
            _service = sevice;
            _mapper = mapper;

        }

        public async Task<CategoryDto> Handle(CategoryCommands request, CancellationToken cancellationToken)
        {
            var entity = await _service.CreateAsync(
                request.CategoryName,
                request.Description,
                cancellationToken
                );
            return _mapper.Map<CategoryDto>(entity);
        }
    }
}
