using Library.Application.Interfaces;
using Library.Domain.Entities;
using MediatR;


namespace Library.Application.Commands
{
    public record CreateBookCommand(string Title, string Author, int Year, string Genre)
       : IRequest<int>;

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository _repository;

        public CreateBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Year = request.Year,
                Genre = request.Genre
            };

            await _repository.AddAsync(book);

            return book.Id;
        }
    }
}
