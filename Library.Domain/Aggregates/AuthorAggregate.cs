using Library.Domain.Entities;
namespace Library.Domain.Aggregates
{
    public class AuthorAggregate
    {
        private readonly Author _author;

        private AuthorAggregate(Author author)
        {
            _author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public static AuthorAggregate Create(string firstName, string lastName)
        {
            var author = Author.Create(firstName, lastName);
            return new AuthorAggregate(author);
        }

        public void UpdateName(string firstName, string lastName)
        {
            _author.UpdateName(firstName, lastName);
        }

        public Author Author => _author;
        public IReadOnlyCollection<BookAuthor> BookAuthors => _author.BookAuthors;
        public string FullName => _author.FullName;
    }
}