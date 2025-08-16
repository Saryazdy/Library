using Library.Application.Interfaces;
using MediatR;


namespace Library.Application.Queries.GetBooks
{

    public record GetBooksQuery() : IRequest<List<BookDto>>;

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository _repository;

        public GetBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync();

            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Year = b.Year,
                Genre = b.Genre
            }).ToList();
        }
    }
}
