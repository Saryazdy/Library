using Library.Domain.Aggregates;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
namespace Library.Domain.Aggregates
{
    public class BookAggregate
    {
        private readonly Book _book;

        private BookAggregate(Book book)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
        }

        // Factory برای ایجاد Book
        public static BookAggregate Create(string title, int year, Genre genre, Isbn? isbn = null, string? description = null)
        {
            var book = Book.Create(title, year, genre, isbn, description);
            return new BookAggregate(book);
        }
        public static BookAggregate FromEntity(Book book) => new BookAggregate(book);
        public static BookAggregate CreateFromEntity(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            return new BookAggregate(book);
        }
        // تغییر اطلاعات Book
        public void Update(string title, int year, Genre genre, Isbn? isbn = null, string? description = null)
        {
            _book.Update(title, year, genre, isbn, description);
        }

        // مدیریت Authors از طریق BookAuthor
        public void AddAuthor(AuthorAggregate authorAggregate)
        {
            if (authorAggregate == null) throw new ArgumentNullException(nameof(authorAggregate));

            var author = authorAggregate.Author;
            if (_book.BookAuthors.Any(ba => ba.AuthorId == author.Id)) return;

            var link = new BookAuthor(_book, author);
            _book.AddBookAuthor(link);
        }

        public void RemoveAuthor(AuthorAggregate authorAggregate)
        {
            if (authorAggregate == null) throw new ArgumentNullException(nameof(authorAggregate));

            var link = _book.BookAuthors.FirstOrDefault(ba => ba.AuthorId == authorAggregate.Author.Id);
            if (link != null) _book.RemoveBookAuthor(link);
        }

        // دسترسی خواندنی
        public Book Book => _book;
        public IReadOnlyCollection<BookAuthor> BookAuthors => _book.BookAuthors;
    }
}
