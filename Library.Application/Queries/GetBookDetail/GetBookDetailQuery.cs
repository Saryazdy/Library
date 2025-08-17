using Library.Application.Books.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GetBookDetail
{
    public sealed record GetBookDetailQuery(Guid Id) : IRequest<BookDto>;
}
