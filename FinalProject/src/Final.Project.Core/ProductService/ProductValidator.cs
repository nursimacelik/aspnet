using Final.Project.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Core.ProductServices
{
    public class CreateProductValidator : AbstractValidator<CreateProductInput>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.UsingStatusId).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
