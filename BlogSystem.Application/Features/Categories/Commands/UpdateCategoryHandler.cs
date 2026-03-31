using BlogSystem.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Commands
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryService _service;
        public UpdateCategoryHandler(ICategoryService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request,cancellationToken);
        }
    }
}
