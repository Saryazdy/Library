using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GetBooksWithPagination
{
    public sealed class GetBooksWithPaginationQueryValidator : AbstractValidator<GetBooksWithPaginationQuery>
    {
        public GetBooksWithPaginationQueryValidator()
        {
            RuleFor(v => v.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(v => v.PageSize).InclusiveBetween(1, 100);
        }
    }
}
