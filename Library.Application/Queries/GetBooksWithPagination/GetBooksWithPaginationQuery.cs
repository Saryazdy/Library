using Library.Application.Books.Dtos;
using Library.Application.Common.Models;
using Library.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GetBooksWithPagination
{
    public sealed record GetBooksWithPaginationQuery(int PageNumber = 1, int PageSize = 10)
        : IRequest<ApiResponse<PaginatedList<BookDto>>>;
}
