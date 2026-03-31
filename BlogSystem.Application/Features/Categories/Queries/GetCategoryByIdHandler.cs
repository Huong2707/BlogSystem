using BlogSystem.Application.DTOs.Categories;
using BlogSystem.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Queries
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryService _service;
        public GetCategoryByIdHandler(ICategoryService service)
        {
            _service = service;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id,cancellationToken);
        }
    }
}
