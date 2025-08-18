using AutoMapper;
using Library.Application._Extensions;
using Library.Application.Books.Dtos;
using Library.Application.Common.Exceptions;
using Library.Application.Common.Interfaces;
using Library.Application.Common.Specifications;
using Library.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
 
namespace Library.Application.Queries.GetBookDetail
{
    public sealed class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDto>
    {
        private readonly IRepository<BookAggregate> _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetBookDetailQueryHandler(IRepository<BookAggregate> repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo; _mapper = mapper; _cache = cache;
        }

        public async Task<BookDto> Handle(GetBookDetailQuery request, CancellationToken ct)
        {
            var cacheKey = $"book-detail-{request.Id}";
            if (_cache.TryGetValue(cacheKey, out BookDto? cached))
                return cached!;

            var spec = new BookWithAuthorsSpec(request.Id);
            var book = await _repo.FirstOrDefaultAsync(spec, ct)
                       ?? throw new NotFoundException("Book", request.Id);

            var dto = _mapper.Map<BookDto>(book);
            _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
            return dto;
        }
    }
}
