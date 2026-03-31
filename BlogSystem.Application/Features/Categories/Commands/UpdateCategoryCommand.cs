using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string CateSlug { get; set; }
        
        public string? CategoryDescription { get; set; }
        public byte IsActive { get; set; }

       
    }
}
