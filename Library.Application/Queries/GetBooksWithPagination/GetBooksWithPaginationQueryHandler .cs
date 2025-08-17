using AutoMapper;
using Library.Application.Books.Dtos;
using Library.Application.Common.Interfaces;
using Library.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GetBooksWithPagination
{
    public sealed class GetBooksWithPaginationQueryHandler
        : IRequestHandler<GetBooksWithPaginationQuery, PaginatedList<BookDto>>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public GetBooksWithPaginationQueryHandler(IApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BookDto>> Handle(GetBooksWithPaginationQuery request, CancellationToken ct)
        {
            var query = _ctx.Books.Select(b => b.Book); // از Aggregate به Entity نگاشت می‌گیریم

            // اگر EF Core داری، می‌تونی مستقیم ProjectTo<BookDto> کنی.
            var projected = query.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Genre = b.Genre.ToString(),
                Isbn = b.Isbn.ToString(),
                Authors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList()
            });

            return await PaginatedList<BookDto>.CreateAsync(projected, request.PageNumber, request.PageSize, ct);
        }
    }
}
