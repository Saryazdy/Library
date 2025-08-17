using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(v => v.Title).NotEmpty().MaximumLength(200);
            RuleFor(v => v.Year).GreaterThan(0);
            RuleFor(v => v.Isbn).NotEmpty();
        }
    }
}
