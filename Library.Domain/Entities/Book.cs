using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Events;
using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

public class Book : AuditableEntity, IHasDomainEvent
{
    public string Title { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public Genre Genre { get; private set; } = Genre.Unknown;
    public Isbn? Isbn { get; private set; }
    public string? Description { get; private set; }
    public DateTime PublishedOn { get; private set; }

    private readonly List<BookAuthor> _bookAuthors = new();
  
    public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

    private Book() { }

    internal static Book Create(string title, int year, Genre genre, Isbn? isbn = null, string? description = null)
    {
        var book = new Book();
        book.Update(title, year, genre, isbn, description, raiseEvent: true);
        return book;
    }

    internal void Update(string title, int year, Genre genre, Isbn? isbn, string? description, bool raiseEvent = true)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty.");

        Title = title.Trim();
        Year = year;
        Genre = genre;
        Isbn = isbn;
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();

        if (raiseEvent)
            AddDomainEvent(new BookUpdatedEvent(this));
    }

    internal void AddBookAuthor(BookAuthor bookAuthor)
    {
        if (!_bookAuthors.Contains(bookAuthor))
            _bookAuthors.Add(bookAuthor);
    }

    internal void RemoveBookAuthor(BookAuthor bookAuthor)
    {
        if (_bookAuthors.Contains(bookAuthor))
            _bookAuthors.Remove(bookAuthor);
    }
}