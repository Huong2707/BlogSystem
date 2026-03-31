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
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryDto>>
    {
        private readonly ICategoryService _service;
        public GetAllCategoryHandler(ICategoryService service)
        {
            _service = service;
        }
        public async Task<List<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
