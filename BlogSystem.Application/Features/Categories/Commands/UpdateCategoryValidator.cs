using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Features.Categories.Commands
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.CategoryName)
                   .NotEmpty()
                   .WithMessage("Name's Category is required")
                   .MaximumLength(100);
            RuleFor(c => c.CateSlug)
                    .NotEmpty()
                    .WithMessage("Slug's Category is required");
            RuleFor(c => c.CategoryDescription)
                   .MaximumLength(255);
            RuleFor(c => c.IsActive)
                   .Must(c => c == 0 || c == 1)
                   .WithMessage("IsActive only accept 0 or 1");

        }
    }
}
