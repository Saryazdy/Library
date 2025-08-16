using Library.Domain.Common;
using Library.Domain.Enums;
using Library.Domain.Events;
using Library.Domain.Exceptions;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book : AuditableEntity, IHasDomainEvent
    {
        public string Title { get; private set; } = string.Empty;
        public int Year { get; private set; }
        public Genre Genre { get; private set; } = Genre.Unknown;
        public Isbn? Isbn { get; private set; }
        public string? Description { get; private set; }

        private readonly List<BookAuthor> _bookAuthors = new();
        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

        // Domain Events از BaseEntity میاد؛ اینجا فقط exposure اینترفیسه
        public IReadOnlyCollection<BaseEvent> DomainEvents => base.DomainEvents;

        private Book() { }

        private Book(string title, int year, Genre genre, Isbn? isbn, string? description)
        {
            Update(title, year, genre, isbn, description, raiseEvent: false);
            AddDomainEvent(new BookCreatedEvent(this));
        }

        public static Book Create(string title, int year, Genre genre, Isbn? isbn = null, string? description = null)
            => new(title, year, genre, isbn, description);

        public void Update(string title, int year, Genre genre, Isbn? isbn, string? description, bool raiseEvent = true)
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

        public void AddAuthor(Author author)
        {
            if (_bookAuthors.Any(x => x.AuthorId == author.Id)) return;
            _bookAuthors.Add(new BookAuthor(this, author));
        }

        public void RemoveAuthor(Author author)
        {
            var link = _bookAuthors.FirstOrDefault(x => x.AuthorId == author.Id);
            if (link is not null) _bookAuthors.Remove(link);
        }

        public void MarkDeleted()
        {
            AddDomainEvent(new BookDeletedEvent(this));
        }
    }
}
