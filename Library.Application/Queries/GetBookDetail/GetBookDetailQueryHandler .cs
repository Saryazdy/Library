using AutoMapper;
using Library.Application._Extensions;
using Library.Application.Books.Dtos;
using Library.Application.Common.Exceptions;
using Library.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GetBookDetail
{
    public sealed class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDto>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public GetBookDetailQueryHandler(IApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx; _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookDetailQuery request, CancellationToken ct)
        {
            var agg = await _ctx.Books.FirstOrDefaultAsync(b => b.Book.Id == request.Id, ct)
                      ?? throw new NotFoundException("Book", request.Id);

            // اگر AutoMapper روی Book → BookDto تنظیم شده:
            var dto = _mapper.Map<BookDto>(agg.Book);
            return dto;
        }
    }
}
