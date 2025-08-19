using Library.Domain.Aggregates;
using Library.Domain.Entities;

public class BookAuthor
{
    private BookAuthor() { } // EF Core

    internal BookAuthor(Book book, Author author)
    {
        Book = book ?? throw new ArgumentNullException(nameof(book));
        BookId = book.Id;
        Author= author ?? throw new ArgumentNullException(nameof(author));
        AuthorId = author.Id;
    }

    public Guid BookId { get; private set; }
    public Book Book { get; private set; } = default!;

    public Guid AuthorId { get; private set; }
    public Author Author { get; private set; } = default!;
}