using BlogSystem.Application.DTOs.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryDto>>
    {

    }
}
