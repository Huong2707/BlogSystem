using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Commands
{
    public class CategoryValidator :AbstractValidator<CategoryCommands>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Name's Category is required")
             .MaximumLength(100);
            RuleFor(c => c.Description)
                .MaximumLength(255);
        }
    }
}
